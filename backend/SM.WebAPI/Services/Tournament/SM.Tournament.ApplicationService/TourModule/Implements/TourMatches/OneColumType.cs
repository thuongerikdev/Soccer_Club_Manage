using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.Domain.Club.Club;
using SM.Tournament.Domain.Match;
using SM.Tournament.Domain.Tournament;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SM.Tournament.ApplicationService.TourModule.Abtracts;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic.SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos;
using SM.Tournament.Infrastructure;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using SM.Tournament.Dtos.OrderDto;
using SM.Tournament.Dtos.TournamentDto.Tournament;

namespace SM.Tournament.ApplicationService.TourModule.Implements.TourMatches
{
    public class OneColumType : TournamentServiceBase, ITourMatchStrategy
    {
        private readonly IMatchesStatisticStrategy _matches;
        private readonly IOrderService _orderService;
        private readonly ITournamentService _tournamentService;

        public OneColumType(ILogger<OneColumType> logger, TournamentDbContext dbContext,
            IOrderService orderService,
            ITournamentService tournamentService,
            [FromKeyedServices("matches")] IMatchesStatisticStrategy matches)
            : base(logger, dbContext)
        {
            _matches = matches;
            _orderService = orderService;
            _tournamentService = tournamentService;
        }

        public async Task<TournamentResponeDto> CreateTournamentMatch(int tournamentID)
        {
            var order = await _orderService.getOrderByTour(tournamentID);
            var orderData = order.Data as CreateOrderDto;
            if (orderData.PaymentStatus == "Pending" && orderData.OrderStatus == "Pending")
            {
                return new TournamentResponeDto
                {
                    Data = null,
                    ErrorCode = 1,
                    ErrorMessage = "Order is pending"
                };
            }
            

                var listClub = await _dbContext.TournamentClubs
                .Where(x => x.TournamentID == tournamentID)
                .ToListAsync();
            var tournament = await _tournamentService.getTournament(tournamentID);
            var tournamentData = tournament.Data as CreateTournamentDto;
            if (listClub.Count < tournamentData.numberMember)
            {
                return new TournamentResponeDto
                {
                    Data = null,
                    ErrorCode = 1,
                    ErrorMessage = "Not enough clubs to create matches"
                };
            }
            var clubTeams = new List<ClubTeam>();
            foreach (var team in listClub)
            {
                var clubTeam = await _dbContext.ClubTeams.FirstOrDefaultAsync(x => x.ClubID == team.ClubID);
                if (clubTeam != null)
                {
                    clubTeams.Add(clubTeam);
                }
            }

            int n = clubTeams.Count;

            // Add a "bye" if the number of clubs is odd
            if (n % 2 == 1)
            {
                clubTeams.Add(new ClubTeam
                {
                    ClubID = 0,
                    ClubName = "Bye",
                    ClubAge = "0",
                    ClubBanner = "0",
                    ClubLogo = "0",
                    ClubDescription = "No match",
                    Budget = 0,
                    ClubLevel = "0",
                    UserID = 0
                });
                n++;
            }

            var matches = new List<Matches>();

            // Create matches for each round
            for (int round = 0; round < n - 1; round++)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    ClubTeam home = clubTeams[i];
                    ClubTeam away = clubTeams[n - 1 - i];

                    if (home.ClubName != "Bye" && away.ClubName != "Bye")
                    {
                        matches.Add(new Matches
                        {
                            TeamA = home.ClubID,
                            TeamB = away.ClubID,
                            TournamentID = tournamentID,
                            Round = round + 1,
                            StartTime = DateTime.Now.AddDays(1),
                            EndTime = DateTime.Now.AddDays(1).AddMinutes(90),
                            Stadium = "Stadium Name",
                            MatchesName = "vòng bảng",
                            MatchesDescription = $"{home.ClubName} vs {away.ClubName}",
                            IsFinish = false,
                            AwayScore = 0,
                            HomeScore = 0
                        });
                    }
                }

                // Rotate teams
                var last = clubTeams[^1];
                clubTeams.RemoveAt(n - 1);
                clubTeams.Insert(1, last);
            }

            await _dbContext.Matches.AddRangeAsync(matches);
            await _dbContext.SaveChangesAsync();

            return new TournamentResponeDto
            {
                Data = matches,
                ErrorCode = 0,
                ErrorMessage = "Tournament matches created successfully"
            };
        }

        public async Task<TournamentResponeDto> ProcessMatchResult(int matchID)
        {
            var matchStat = await _matches.getStatisTic(new ReadMatchesStatisticDto
            {
                MatchesID = matchID
            });

            var teamData = matchStat.Data as MatchStatisticsDto;

            int teamAStatH1 = teamData.Half1.TeamA.Goal;
            int teamBStatH1 = teamData.Half1.TeamB.Goal;
            int teamAStatH2 = teamData.Half2.TeamA.Goal;
            int teamBStatH2 = teamData.Half2.TeamB.Goal;

            var totalTeamA = teamAStatH1 + teamAStatH2;
            var totalTeamB = teamBStatH1 + teamBStatH2;

            var match = await _dbContext.Matches.FirstOrDefaultAsync(m => m.MatchesID == matchID);

            if (match == null || match.IsFinish)
                return new TournamentResponeDto
                {
                    Data = null,
                    ErrorCode = 1,
                    ErrorMessage = "Match not found or already finished"
                };

            match.HomeScore = totalTeamA;
            match.AwayScore = totalTeamB;
            match.IsFinish = true;

            var homeStanding = await _dbContext.TournamentClubs.FirstOrDefaultAsync(ts => ts.ClubID == match.TeamA);
            var awayStanding = await _dbContext.TournamentClubs.FirstOrDefaultAsync(ts => ts.ClubID == match.TeamB);

            if (homeStanding == null || awayStanding == null)
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Standing not found",
                    Data = null
                };

            // Update standings
            homeStanding.Played++;
            awayStanding.Played++;
            homeStanding.GoalFor += totalTeamA;
            homeStanding.GoatGet += totalTeamB;
            awayStanding.GoalFor += totalTeamA;
            awayStanding.GoatGet += totalTeamB;

            if (totalTeamA > totalTeamB)
            {
                homeStanding.Won++;
                homeStanding.Points += 3;
                awayStanding.Lost++;
            }
            else if (totalTeamA < totalTeamB)
            {
                awayStanding.Won++;
                awayStanding.Points += 3;
                homeStanding.Lost++;
            }
            else
            {
                homeStanding.Drawn++;
                awayStanding.Drawn++;
                homeStanding.Points++;
                awayStanding.Points++;
            }

            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Match result processed successfully",
                Data = match.MatchesID
            };
        }

        public async Task<TournamentResponeDto> GetStandings(int tournamentID)
        {
            var standings = await _dbContext.TournamentClubs
                .Where(x => x.TournamentID == tournamentID)
                .OrderByDescending(ts => ts.Points)
                .ThenByDescending(ts => ts.GoalFor - ts.GoatGet)
                .ThenByDescending(ts => ts.GoalFor)
                .ToListAsync();

            return new TournamentResponeDto
            {
                Data = standings,
                ErrorCode = 0,
                ErrorMessage = "Standings retrieved successfully"
            };
        }

        public async Task<TournamentResponeDto> CreateSemiFinalMatches(int tournamentID)
        {
            var standingsResp = await GetStandings(tournamentID);
            var topTeams = standingsResp.Data as List<TournamentClub>;

            if (topTeams.Count < 4)
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Not enough teams for semi-finals.",
                    Data = null
                };

            var semiFinalMatches = new List<Matches>
            {
                new Matches
                {
                    TeamA = topTeams[0].ClubID,
                    TeamB = topTeams[3].ClubID,
                    Round = 1,
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1).AddMinutes(90),
                    Stadium = "Stadium A",
                    MatchesName = "Bán kết 1",
                    MatchesDescription = "Bán kết",
                    IsFinish = false,
                    TournamentID = tournamentID
                },
                new Matches
                {
                    TeamA = topTeams[1].ClubID,
                    TeamB = topTeams[2].ClubID,
                    Round = 1,
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1).AddMinutes(90),
                    Stadium = "Stadium B",
                    MatchesName = "Bán kết 2",
                    MatchesDescription = "Bán kết",
                    IsFinish = false,
                    TournamentID = tournamentID
                }
            };

            await _dbContext.Matches.AddRangeAsync(semiFinalMatches);
            await _dbContext.SaveChangesAsync();

            return new TournamentResponeDto
            {
                Data = semiFinalMatches,
                ErrorCode = 0,
                ErrorMessage = "Semi-final matches created successfully"
            };
        }

        public async Task<TournamentResponeDto> CreateFinalAndThirdPlaceMatches(int tournamentID)
        {
            var semiFinalMatches = await _dbContext.Matches
                .Where(m => m.Round == 1 && m.IsFinish && m.TournamentID == tournamentID && m.MatchesName == "Bán kết")
                .ToListAsync();

            if (semiFinalMatches.Count != 2)
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Not enough results from semi-finals.",
                    Data = null
                };

            var finalMatch = new Matches
            {
                TeamA = semiFinalMatches[0].HomeScore > semiFinalMatches[0].AwayScore
                    ? semiFinalMatches[0].TeamA
                    : semiFinalMatches[0].TeamB,
                TeamB = semiFinalMatches[1].HomeScore > semiFinalMatches[1].AwayScore
                    ? semiFinalMatches[1].TeamA
                    : semiFinalMatches[1].TeamB,
                Round = 2,
                StartTime = DateTime.Now.AddDays(2),
                EndTime = DateTime.Now.AddDays(2).AddMinutes(90),
                Stadium = "Final Stadium",
                MatchesName = "Chung kết",
                MatchesDescription = "Trận tranh ngôi vô địch",
                IsFinish = false,
                TournamentID = tournamentID
            };

            var thirdPlaceMatch = new Matches
            {
                TeamA = semiFinalMatches[0].HomeScore > semiFinalMatches[0].AwayScore
                    ? semiFinalMatches[0].TeamB
                    : semiFinalMatches[0].TeamA,
                TeamB = semiFinalMatches[1].HomeScore > semiFinalMatches[1].AwayScore
                    ? semiFinalMatches[1].TeamB
                    : semiFinalMatches[1].TeamA,
                Round = 2,
                StartTime = DateTime.Now.AddDays(2),
                EndTime = DateTime.Now.AddDays(2).AddMinutes(90),
                Stadium = "Third Place Stadium",
                MatchesName = "Tranh hạng 3",
                MatchesDescription = "Trận tranh hạng 3",
                IsFinish = false,
                TournamentID = tournamentID
            };

            await _dbContext.Matches.AddRangeAsync(finalMatch, thirdPlaceMatch);
            await _dbContext.SaveChangesAsync();

            return new TournamentResponeDto
            {
                Data = new { FinalMatch = finalMatch, ThirdPlaceMatch = thirdPlaceMatch },
                ErrorCode = 0,
                ErrorMessage = "Final and third place matches created successfully"
            };
        }

        public async Task<TournamentResponeDto> DetermineFinalRankings(int tournamentID)
        {
            var finalMatch = await _dbContext.Matches
                .FirstOrDefaultAsync(m => m.MatchesName == "Chung kết" && m.IsFinish && m.TournamentID == tournamentID);
            var thirdPlaceMatch = await _dbContext.Matches
                .FirstOrDefaultAsync(m => m.MatchesName == "Tranh hạng 3" && m.IsFinish);

            if (finalMatch == null || thirdPlaceMatch == null)
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Final match or third place match results not found.",
                    Data = null
                };

            var rankings = new List<TournamentClub>();

            var champion = await _dbContext.TournamentClubs.FirstOrDefaultAsync(tc => tc.ClubID == (finalMatch.HomeScore > finalMatch.AwayScore ? finalMatch.TeamA : finalMatch.TeamB));
            var runnerUp = await _dbContext.TournamentClubs.FirstOrDefaultAsync(tc => tc.ClubID == (finalMatch.HomeScore > finalMatch.AwayScore ? finalMatch.TeamB : finalMatch.TeamA));
            var thirdPlace = await _dbContext.TournamentClubs.FirstOrDefaultAsync(tc => tc.ClubID == (thirdPlaceMatch.HomeScore > thirdPlaceMatch.AwayScore ? thirdPlaceMatch.TeamA : thirdPlaceMatch.TeamB));
            var fourthPlace = await _dbContext.TournamentClubs.FirstOrDefaultAsync(tc => tc.ClubID == (thirdPlaceMatch.HomeScore > thirdPlaceMatch.AwayScore ? thirdPlaceMatch.TeamB : thirdPlaceMatch.TeamA));

            rankings.Add(champion);
            rankings.Add(runnerUp);
            rankings.Add(thirdPlace);
            rankings.Add(fourthPlace);

            return new TournamentResponeDto
            {
                Data = rankings,
                ErrorCode = 0,
                ErrorMessage = "Final rankings determined successfully"
            };
        }
    }
}
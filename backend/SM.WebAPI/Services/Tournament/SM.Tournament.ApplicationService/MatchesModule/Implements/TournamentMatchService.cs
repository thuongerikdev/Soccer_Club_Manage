//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using SM.Tournament.ApplicationService.Common;
//using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
//using SM.Tournament.Domain.Club.Club;
//using SM.Tournament.Domain.Match;
//using SM.Tournament.Dtos;
//using SM.Tournament.Dtos.MatchDto.MatchesStatistic.SM.Tournament.Dtos.MatchDto.MatchesStatistic;
//using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
//using SM.Tournament.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using SM.Tournament.Domain.Tournament;
//using System.Text.RegularExpressions;

//namespace SM.Tournament.ApplicationService.MatchesModule.Implements
//{
//    public class TournamentMatchService : TournamentServiceBase 
//    {
//        private readonly IMatchesStatisticStrategy _matches;
//        public TournamentMatchService(ILogger<TournamentMatchService> logger, TournamentDbContext dbContext, [FromKeyedServices("matches")] IMatchesStatisticStrategy matches)
//            : base(logger, dbContext)
//        {
//        }

//        public async Task<List<Matches>> CreateTournamentMatch(int tournamentID, List<ClubTeam> clubs)
//        {
//            var listClub = _dbContext.TournamentClubs.Where(x => x.TournamentID == tournamentID).ToList();
//            int n = listClub.Count;

//            // Add a "bye" if the number of clubs is odd
//            if (n % 2 == 1)
//            {
//                clubs.Add(new ClubTeam
//                {
//                    ClubID = 0,
//                    ClubName = "Bye",
//                    ClubAge = "0",
//                    ClubBanner = "0",
//                    ClubLogo = "0",
//                    ClubDescription = "No match",
//                    Budget = 0,
//                    ClubLevel = "0",
//                    UserID = 0
//                });
//                n++;
//            }

//            int rounds = n - 1;
//            var matches = new List<Matches>();

//            for (int round = 0; round < rounds; round++)
//            {
//                for (int i = 0; i < n / 2; i++)
//                {
//                    ClubTeam home = clubs[i];
//                    ClubTeam away = clubs[n - 1 - i];

//                    if (home.ClubName != "Bye" && away.ClubName != "Bye")
//                    {
//                        matches.Add(new Matches
//                        {
//                            TeamA = home.ClubID,
//                            TeamB = away.ClubID,
//                            TournamentID = tournamentID,
//                            Round = round + 1,
//                            StartTime = DateTime.Now.AddDays(1),
//                            EndTime = DateTime.Now.AddDays(1).AddMinutes(90), // Fixed the typo here
//                            Stadium = "0",
//                            MatchesName = $"{home.ClubName} vs {away.ClubName}",
//                            MatchesDescription = "Match description",
//                            IsFinish = false,
//                        });
//                    }
//                }

//                // Rotate the teams (except the first one)
//                var last = clubs[^1]; // Last team
//                clubs.RemoveAt(n - 1); // Remove last team
//                clubs.Insert(1, last); // Reinsert it in the second position
//            }

//            return matches;
//        }
//        public async Task ProcessMatchResult(int matchID)
//        {

//            var matchStat = await _matches.getStatisTic(new ReadMatchesStatisticDto
//            {
//                MatchesID = matchID
//            });

//            var teamData = matchStat.Data as MatchStatisticsDto;

//            int teamAStatH1 = teamData.Half1.TeamA.Goal;
//            int teamBStatH1 = teamData.Half1.TeamB.Goal;
//            int teamAStatH2 = teamData.Half2.TeamA.Goal;
//            int teamBStatH2 = teamData.Half2.TeamB.Goal;

//            var totalTeamA = teamAStatH1 + teamAStatH2;
//            var totalTeamB = teamBStatH1 + teamBStatH2;

           

//            var match = await _dbContext.Matches.FirstOrDefaultAsync(m => m.MatchesID == matchID);

//            if (match == null || match.IsFinish)
//                return;

//            match.HomeScore = totalTeamA;
//            match.AwayScore = totalTeamB;
//            match.IsFinish = true;

//            var homeStanding = await _dbContext.TournamentClubs.FirstOrDefaultAsync(ts => ts.ClubID == match.TeamA);
//            var awayStanding = await _dbContext.TournamentClubs.FirstOrDefaultAsync(ts => ts.ClubID == match.TeamB);

//            if (homeStanding == null || awayStanding == null) return;

//            homeStanding.Played++;
//            awayStanding.Played++;

//            homeStanding.GoalFor += totalTeamA;
//            homeStanding.GoatGet += totalTeamB;

//            awayStanding.GoalFor += totalTeamA;
//            awayStanding.GoatGet += totalTeamB;

//            if (totalTeamA > totalTeamB)
//            {
//                homeStanding.Won++;
//                homeStanding.Points += 3;
//                awayStanding.Lost++;
//            }
//            else if (totalTeamA < totalTeamB)
//            {
//                awayStanding.Won++;
//                awayStanding.Points += 3;
//                homeStanding.Lost++;
//            }
//            else
//            {
//                homeStanding.Drawn++;
//                awayStanding.Drawn++;
//                homeStanding.Points++;
//                awayStanding.Points++;
//            }

//            await _dbContext.SaveChangesAsync();
//        }
//        public async Task<List<TournamentClub>> GetStandings()
//        {

//            return await _dbContext.TournamentClubs

//                .Include(ts => ts.ClubID)
//                .OrderByDescending(ts => ts.Points)
//                .ThenByDescending(ts => ts.GoalFor - ts.GoatGet)
//                .ThenByDescending(ts => ts.GoalFor)
//                .ToListAsync();
//        }
//        // Tạo trận bán kết
//        public async Task<List<Matches>> CreateSemiFinalMatches()
//        {
//            var standings = await GetStandings();
//            var topTeams = standings.Take(4).ToList();

//            if (topTeams.Count < 4)
//                throw new Exception("Không đủ đội để tạo bán kết.");

//            var semiFinalMatches = new List<Matches>
//            {
//                new Matches
//                {
//                    TeamA = topTeams[0].ClubID,
//                    TeamB = topTeams[3].ClubID,
//                    Round = 1,
//                    StartTime = DateTime.Now.AddDays(1),
//                    EndTime = DateTime.Now.AddDays(1).AddMinutes(90),
//                    Stadium = "Stadium A",
//                    MatchesName = $"{topTeams[0].ClubID} vs {topTeams[3].ClubID}",
//                    MatchesDescription = "Bán kết 1",
//                    IsFinish = false
//                },
//                new Matches
//                {
//                    TeamA = topTeams[1].ClubID,
//                    TeamB = topTeams[2].ClubID,
//                    Round = 1,
//                    StartTime = DateTime.Now.AddDays(1),
//                    EndTime = DateTime.Now.AddDays(1).AddMinutes(90),
//                    Stadium = "Stadium B",
//                    MatchesName = $"{topTeams[1].ClubID}  vs  {topTeams[2].ClubID}",
//                    MatchesDescription = "Bán kết 2",
//                    IsFinish = false
//                }
//            };

//            await _dbContext.Matches.AddRangeAsync(semiFinalMatches);
//            await _dbContext.SaveChangesAsync();

//            return semiFinalMatches;
//        }

//        // Tạo trận chung kết và tranh hạng 3
//        public async Task CreateFinalAndThirdPlaceMatches()
//        {
//            var semiFinalMatches = await _dbContext.Matches
//                .Where(m => m.Round == 1 && m.IsFinish)
//                .ToListAsync();

//            if (semiFinalMatches.Count != 2)
//                throw new Exception("Chưa có đủ kết quả bán kết.");

//            var finalMatch = new Matches
//            {
//                TeamA = semiFinalMatches[0].HomeScore > semiFinalMatches[0].AwayScore
//                    ? semiFinalMatches[0].TeamA
//                    : semiFinalMatches[0].TeamB,
//                TeamB = semiFinalMatches[1].HomeScore > semiFinalMatches[1].AwayScore
//                    ? semiFinalMatches[1].TeamA
//                    : semiFinalMatches[1].TeamB,
//                Round = 2, // Chung kết
//                StartTime = DateTime.Now.AddDays(2),
//                EndTime = DateTime.Now.AddDays(2).AddMinutes(90),
//                Stadium = "Final Stadium",
//                MatchesName = "Chung kết",
//                MatchesDescription = "Trận tranh ngôi vô địch",
//                IsFinish = false
//            };

//            var thirdPlaceMatch = new Matches
//            {
//                TeamA = semiFinalMatches[0].HomeScore > semiFinalMatches[0].AwayScore
//                    ? semiFinalMatches[0].TeamB
//                    : semiFinalMatches[0].TeamA,
//                TeamB = semiFinalMatches[1].HomeScore > semiFinalMatches[1].AwayScore
//                    ? semiFinalMatches[1].TeamB
//                    : semiFinalMatches[1].TeamA,
//                Round = 2, // Tranh hạng 3
//                StartTime = DateTime.Now.AddDays(2),
//                EndTime = DateTime.Now.AddDays(2).AddMinutes(90),
//                Stadium = "Third Place Stadium",
//                MatchesName = "Tranh hạng 3",
//                MatchesDescription = "Trận tranh hạng 3",
//                IsFinish = false
//            };

//            await _dbContext.Matches.AddRangeAsync(finalMatch, thirdPlaceMatch);
//            await _dbContext.SaveChangesAsync();
//        }

//        // Xác định thứ hạng cuối cùng
//        public async Task<List<TournamentClub>> DetermineFinalRankings()
//        {
//            var finalMatch = await _dbContext.Matches
//                .FirstOrDefaultAsync(m => m.MatchesName == "Chung kết" && m.IsFinish);
//            var thirdPlaceMatch = await _dbContext.Matches
//                .FirstOrDefaultAsync(m => m.MatchesName == "Tranh hạng 3" && m.IsFinish);

//            if (finalMatch == null || thirdPlaceMatch == null)
//                throw new Exception("Chưa có kết quả trận chung kết hoặc tranh hạng 3.");

//            var rankings = new List<TournamentClub>();

//            var champion = await _dbContext.TournamentClubs.FirstOrDefaultAsync(tc => tc.ClubID == (finalMatch.HomeScore > finalMatch.AwayScore ? finalMatch.TeamA : finalMatch.TeamB));
//            var runnerUp = await _dbContext.TournamentClubs.FirstOrDefaultAsync(tc => tc.ClubID == (finalMatch.HomeScore > finalMatch.AwayScore ? finalMatch.TeamB : finalMatch.TeamA));
//            var thirdPlace = await _dbContext.TournamentClubs.FirstOrDefaultAsync(tc => tc.ClubID == (thirdPlaceMatch.HomeScore > thirdPlaceMatch.AwayScore ? thirdPlaceMatch.TeamA : thirdPlaceMatch.TeamB));
//            var fourthPlace = await _dbContext.TournamentClubs.FirstOrDefaultAsync(tc => tc.ClubID == (thirdPlaceMatch.HomeScore > thirdPlaceMatch.AwayScore ? thirdPlaceMatch.TeamB : thirdPlaceMatch.TeamA));

//            rankings.Add(champion);
//            rankings.Add(runnerUp);
//            rankings.Add(thirdPlace);
//            rankings.Add(fourthPlace);

//            return rankings;
//        }
//    }
//}
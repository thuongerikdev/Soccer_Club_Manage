using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Domain.Club.ClubFund;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund
{
    public class ClubFundService : TournamentServiceBase, IClubFundService
    {
        public ClubFundService(ILogger<ClubFundService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreateClubFund(CreateFundDto createClubFundDto)
        {
            try
            {
                var clubFund = new ClubFunds
                {
                    ClubID = createClubFundDto.ClubID,
                    FundAmount = createClubFundDto.FundAmount,
                    FundDate = createClubFundDto.FundDate,
                    FundDescription = createClubFundDto.FundDescription,
                    FundType = createClubFundDto.FundType,
                    FundStatus = createClubFundDto.FundStatus,
                    FundName = createClubFundDto.FundName
                };
                _dbContext.ClubFunds.Add(clubFund);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Club Fund Created Successfully",
                    ErrorCode = 0,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }

        }
        public async Task<TournamentResponeDto> UpdateClubFund(UpdateFundDto updateClubFundDto)
        {
            try
            {
                var clubFund = _dbContext.ClubFunds.Where(x => x.FundID == updateClubFundDto.FundID).FirstOrDefault();
                if (clubFund == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorMessage = "Club Fund Not Found",
                        ErrorCode = 1,
                        Data = null
                    };
                }
                clubFund.FundAmount = updateClubFundDto.FundAmount;
                clubFund.FundDate = updateClubFundDto.FundDate;
                clubFund.FundDescription = updateClubFundDto.FundDescription;
                clubFund.FundType = updateClubFundDto.FundType;
                clubFund.FundStatus = updateClubFundDto.FundStatus;
                clubFund.FundName = updateClubFundDto.FundName;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Club Fund Updated Successfully",
                    ErrorCode = 0,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> RemoveClubFund(int clubFundId)
        {
            try
            {
                var clubFund = _dbContext.ClubFunds.Where(x => x.FundID == clubFundId).FirstOrDefault();
                if (clubFund == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorMessage = "Club Fund Not Found",
                        ErrorCode = 1,
                        Data = null
                    };
                }
                _dbContext.ClubFunds.Remove(clubFund);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Club Fund Removed Successfully",
                    ErrorCode = 0,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async ValueTask<TournamentResponeDto> GetAllClubFund()
        {
            try
            {
                var clubFunds = _dbContext.ClubFunds.ToList();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Club Fund List",
                    ErrorCode = 0,
                    Data = clubFunds
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async ValueTask<TournamentResponeDto> GetClubFundById(int clubID)
        {
            try
            {
                var clubFund = _dbContext.ClubFunds.Where(x => x.ClubID == clubID).FirstOrDefault();
                if (clubFund == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorMessage = "Club Fund Not Found",
                        ErrorCode = 1,
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorMessage = "Club Fund",
                    ErrorCode = 0,
                    Data = clubFund
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }

    }
}

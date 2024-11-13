using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.Caculate;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Domain.Club.ClubFund;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Caculate
{
    public class CaculateFundsService : TournamentServiceBase, ICaculateService

    {
        private readonly FundFactoryService _fundFactoryService;
        public CaculateFundsService(ILogger<CaculateFundsService> logger, TournamentDbContext dbContext, FundFactoryService fundFactoryService) : base(logger, dbContext)
        {
            _fundFactoryService = fundFactoryService;
        }
        public async Task<TournamentResponeDto> CaculateFunds(CreateActionFundDto createActionFundDto)
        {
            var strategy = _fundFactoryService.Create(createActionFundDto.FundActionType);
            var fund = await _dbContext.ClubFunds.FindAsync(createActionFundDto.FundID);
            if (fund == null) throw new ArgumentException("Quỹ không tồn tại.");

            var command = new FundActionCommand(strategy, fund, createActionFundDto.Amount);
            command.CalculateFund();
            var history = new FundActionHistory
            {
                FundID = fund.FundID, // Ensure this is set correctly
                Amount = createActionFundDto.Amount,
                FundActionType = createActionFundDto.FundActionType,
                ActionDate = DateTime.Now,
                Description = createActionFundDto.Description,
                Name = createActionFundDto.Name,
                PlayerID = createActionFundDto.PlayerID,

            };
            _dbContext.FundActionHistories.Add(history);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorMessage = "Contribution Added Successfully",
                ErrorCode = 0,
                Data = null
            };


        }





    }
}

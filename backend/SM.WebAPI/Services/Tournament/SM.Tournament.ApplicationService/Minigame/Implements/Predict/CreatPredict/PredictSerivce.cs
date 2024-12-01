using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SM.Constant.Tournament;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Predict;
using SM.Tournament.Domain.Match;
using SM.Tournament.Domain.Minigame;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Predict;
using SM.Tournament.Dtos.MinigameDto.Vote;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements.Predict.CreatPredict
{
    public class PredictSerivce : TournamentServiceBase, IPredictService
    {
        private readonly IChooseTypePredict _chooseHalfOrFullTime;
        private readonly IChooseTypePredict _choosePredictionType;
        private readonly IMatchesService _matchesService;
        private readonly IMinigameService _minigameService;

        public PredictSerivce(ILogger<PredictSerivce> logger, TournamentDbContext dbContext,
            [FromKeyedServices(TourConst.HalfOrFullTime)] IChooseTypePredict chooseHalfOrFullTime,
            [FromKeyedServices(TourConst.PredictType)] IChooseTypePredict choosePredictionType,

            IMatchesService matchesService,
            IMinigameService minigameService)
            : base(logger, dbContext)
        {
            _chooseHalfOrFullTime = chooseHalfOrFullTime;
            _choosePredictionType = choosePredictionType;
            _matchesService = matchesService;
            _minigameService = minigameService;
        }

        public async Task<TournamentResponeDto> CreatePredict(CreatePredictDto createPredictDto)
        {
            try
            {
                var minigame = _dbContext.Minigames.Where(x => x.MinigameID == createPredictDto.MinigameID
                                                          && x.MinigameType == TourConst.Predict).FirstOrDefault();
                if (minigame == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Minigame not found",
                        Data = null
                    };
                }
                // Bước 1: Chọn hiệp hoặc toàn trận
                var halfOrFullTimeDto = await _chooseHalfOrFullTime.chooseType(createPredictDto.HalfOrFull, createPredictDto);

                // Bước 2: Chọn thể loại dự đoán
                var predictionTypeDto = await _choosePredictionType.chooseType(createPredictDto.PredictionType, halfOrFullTimeDto);
     
                var minigameEndTime = minigame.EndDates;

                var match= await _matchesService.GetMatches(minigame.MatchesID);
                var matchData = match.Data as Matches;
                var endTime = matchData.EndTime;
                if (DateTime.Now > endTime || DateTime.Now > minigameEndTime)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Time to predict has expired",
                        Data = null
                    };
                }   


                // Lưu dự đoán vào cơ sở dữ liệu
                var predict = new Predictions
                {
                    MinigameID = predictionTypeDto.MinigameID,
                    OddEven = predictionTypeDto.OddEven,
                    UserID = predictionTypeDto.UserID,
                    PredictionDate = predictionTypeDto.PredictionDate,
                    TeamAscore = predictionTypeDto.TeamAscore,
                    TeamBscore = predictionTypeDto.TeamBscore,
                    PredictTotal = predictionTypeDto.PredictTotal,
                    half = predictionTypeDto.half,
                };

                _dbContext.Predictions.Add(predict);
                await _dbContext.SaveChangesAsync();

                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Predict Success",
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

        public async Task<TournamentResponeDto> DeletePredict(int predictionID)
        {
            try
            {
                var predict = await _dbContext.Predictions.FindAsync(predictionID);
                if (predict == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Prediction not found",
                        Data = null
                    };
                }
                _dbContext.Predictions.Remove(predict);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Predict Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetPredict(int predictionID)
        {
            try
            {
                var predict = _dbContext.Predictions.Find(predictionID);
                if (predict == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Prediction not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = predict
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
        public async Task<TournamentResponeDto> UpdatePredict(UpdatePredictDto updatePredictDto)
        {
            try
            {
                var predict = await _dbContext.Predictions.FindAsync(updatePredictDto.PredictionID);
                if (predict == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Prediction not found",
                        Data = null
                    };
                }
                predict.MinigameID = updatePredictDto.MinigameID;

                predict.OddEven = updatePredictDto.OddEven;
                predict.UserID = updatePredictDto.UserID;
                predict.PredictionDate = updatePredictDto.PredictionDate;
                predict.TeamAscore = updatePredictDto.TeamAscore;
                predict.TeamBscore = updatePredictDto.TeamBscore;
                predict.PredictTotal = updatePredictDto.PredictTotal;
                predict.half = updatePredictDto.half;

                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Predict Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetAllPredict()
        {
            try
            {
                var predicts = _dbContext.Predictions.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = predicts
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetPredictsByMinigame(int miniGameID)
        {
            try
            {
                var predicts = _dbContext.Predictions.Where(x => x.MinigameID == miniGameID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = predicts
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetPredictbyID(int predictID)
        {
            try
            {
                var predicts = _dbContext.Predictions.Where(x => x.PredictionID == predictID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = predicts
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetPredictsByUser(int userID)
        {
            try
            {
                var predicts = _dbContext.Predictions.Where(x => x.UserID == userID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = predicts
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }






    }
}

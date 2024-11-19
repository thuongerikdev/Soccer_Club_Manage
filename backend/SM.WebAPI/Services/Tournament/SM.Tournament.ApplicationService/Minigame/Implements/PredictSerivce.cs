using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.Domain.Minigame;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Predict;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements
{
    public class PredictSerivce : TournamentServiceBase, IPredictService
    {
        public PredictSerivce (ILogger<PredictSerivce> logger , TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public  async Task<TournamentResponeDto> CreatePredict(CreatePredictDto createPredictDto)
        {
            try
            {
                var predict = new Predictions
                {
                    MinigameID = createPredictDto.MinigameID,
                    MatchID = createPredictDto.MatchID,
                    UserID = createPredictDto.UserID,
                    Prediction = createPredictDto.Prediction,
                    PredictionDate = createPredictDto.PredictionDate,
                    
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
                predict.MatchID = updatePredictDto.MatchID;
                predict.UserID = updatePredictDto.UserID;
                predict.Prediction = updatePredictDto.Prediction;
                predict.PredictionDate = updatePredictDto.PredictionDate;
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

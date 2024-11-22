using SM.Tournament.ApplicationService.Minigame.Abtracts.Predict;
using SM.Tournament.Dtos.MinigameDto.Predict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements.Predict.CreatPredict
{
    public class PredictionType : IChooseTypePredict
    {
        public async Task<CreatePredictDto> chooseType(string predictionType, CreatePredictDto createPredictDto)
        {
            // Chọn thể loại dự đoán
            return predictionType switch
            {
                "MatchScore" => CreatePredictDtoForMatchScore(createPredictDto),
                "Total" => CreatePredictDtoForTotal(createPredictDto),
                "OddEven" => CreatePredictDtoForOddEven(createPredictDto),
                _ => throw new ArgumentException("Invalid prediction type", nameof(predictionType)),
            };
        }

        private CreatePredictDto CreatePredictDtoForMatchScore(CreatePredictDto createPredictDto)
        {
            return new CreatePredictDto
            {
                PredictTotal = null,
                TeamAscore = createPredictDto.TeamAscore,
                TeamBscore = createPredictDto.TeamBscore,
                MinigameID = createPredictDto.MinigameID,
                UserID = createPredictDto.UserID,
                OddEven = null,
                half = createPredictDto.half,
            };
        }

        private CreatePredictDto CreatePredictDtoForTotal(CreatePredictDto createPredictDto)
        {
            return new CreatePredictDto
            {
                PredictTotal = createPredictDto.PredictTotal,
                TeamAscore = null,
                TeamBscore = null,
                MinigameID = createPredictDto.MinigameID,
                UserID = createPredictDto.UserID,
                OddEven = null,
                half = createPredictDto.half,
            };
        }

        private CreatePredictDto CreatePredictDtoForOddEven(CreatePredictDto createPredictDto)
        {
            return new CreatePredictDto
            {
                PredictTotal = null,
                TeamAscore = null,
                TeamBscore = null,
                MinigameID = createPredictDto.MinigameID,
                UserID = createPredictDto.UserID,
                OddEven = createPredictDto.OddEven,
                half = createPredictDto.half,
            };
        }
    }

}

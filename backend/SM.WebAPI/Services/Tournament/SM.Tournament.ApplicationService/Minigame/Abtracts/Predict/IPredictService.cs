using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Predict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Abtracts.Predict
{
    public interface IPredictService
    {
        public Task<TournamentResponeDto> CreatePredict(CreatePredictDto createPredictDto);
        public Task<TournamentResponeDto> UpdatePredict(UpdatePredictDto updatePredictDto);
        public Task<TournamentResponeDto> DeletePredict(int predictionID);
        public Task<TournamentResponeDto> GetPredictsByMinigame(int minigameID);
        public Task<TournamentResponeDto> GetPredictbyID(int PredictID);
        public Task<TournamentResponeDto> GetAllPredict();
    }
}

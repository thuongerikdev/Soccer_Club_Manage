using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Predict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Abtracts.Predict
{
    public interface IChooseTypePredict
    {
        public Task<CreatePredictDto> chooseType( string type , CreatePredictDto createPredictDto);
    }
}

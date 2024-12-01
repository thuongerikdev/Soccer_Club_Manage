using SM.Constant.Tournament;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Predict;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Predict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements.Predict.CreatPredict
{
    public class halfChoose : IChooseTypePredict
    {
    
            public async Task<CreatePredictDto> chooseType(string type, CreatePredictDto createPredictDto)
            {
                // Xử lý khi chọn hiệp hoặc toàn trận
                switch (type)
                {
                    case TourConst.H1:
                        createPredictDto.half = 1;  // Ví dụ chọn hiệp đầu
                        break;
                    case TourConst.H2:
                        createPredictDto.half = 2;  // Ví dụ chọn toàn trận
                        break;
                    case TourConst.Full:
                        createPredictDto.half = null;  // Ví dụ chọn toàn trận
                        break;
                    default:
                        throw new ArgumentException("Invalid time period", nameof(type));
                }
                return createPredictDto;
            }
        }

    
}

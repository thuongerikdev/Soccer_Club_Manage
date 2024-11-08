using SM.LineUp.Dtos;
using SM.LineUp.Dtos.LineUpDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.ApplicationService.Module.Abtracts
{
    public  interface  ILineUpService
    {
        public Task<LineUpResponeDto> CreateLineUp(CreateLineUpDto createLineUpDto);
        public Task<LineUpResponeDto> UpdateLineUp(UpdateLineUpDto updateLineUpDto);
        public Task<LineUpResponeDto> RemoveLineUp(int lineUpId);
        public ValueTask<LineUpResponeDto> GetAllLineUp();
        public ValueTask<LineUpResponeDto> GetLineUpById(int lineUpId);


    }
}

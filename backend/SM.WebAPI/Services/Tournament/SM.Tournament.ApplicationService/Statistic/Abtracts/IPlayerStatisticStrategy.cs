using SM.Tournament.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Statistic.Abtracts
{
    public interface IPlayerStatisticStrategy
    {
        public Task<TournamentResponeDto> GetStatistic(int ID);
    }
}

using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic
{
    public interface IMatchStatisticUse
    {
        public IMatchesStatisticStrategy ChooseStatistic(string type);
    }
}

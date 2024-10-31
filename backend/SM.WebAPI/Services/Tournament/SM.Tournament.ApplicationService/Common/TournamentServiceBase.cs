using Microsoft.Extensions.Logging;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Common
{
    public class TournamentServiceBase
    {
       
            protected readonly ILogger _logger;
            protected readonly TournamentDbContext _dbContext;
            protected TournamentServiceBase(ILogger logger, TournamentDbContext dbContext)
            {
                _logger = logger;
                _dbContext = dbContext;
            }

        
    }
}

using Microsoft.Extensions.Logging;
using SM.Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.ApplicationService.Common
{
    public class MatchServiceBase
    {
       
            protected readonly ILogger _logger;
            protected readonly MatchDbContext _dbContext;
            protected MatchServiceBase(ILogger logger, MatchDbContext dbContext)
            {
                _logger = logger;
                _dbContext = dbContext;
            }

        
    }
}

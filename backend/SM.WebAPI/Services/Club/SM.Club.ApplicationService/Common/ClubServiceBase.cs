using Microsoft.Extensions.Logging;
using SM.Club.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Club.ApplicationService.Common
{
    public class ClubServiceBase
    {
       
            protected readonly ILogger _logger;
            protected readonly ClubDbContext _dbContext;
            protected ClubServiceBase(ILogger logger, ClubDbContext dbContext)
            {
                _logger = logger;
                _dbContext = dbContext;
            }

        
    }
}

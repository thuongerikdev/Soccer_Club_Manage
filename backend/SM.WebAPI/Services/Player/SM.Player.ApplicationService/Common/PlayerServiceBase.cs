
using Microsoft.Extensions.Logging;
using SM.Player.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Player.ApplicationService.Common
{
    public class PlayerServiceBase
    {
       
            protected readonly ILogger _logger;
            protected readonly PlayerDbContext _dbContext;
            protected PlayerServiceBase(ILogger logger, PlayerDbContext dbContext)
            {
                _logger = logger;
                _dbContext = dbContext;
            }

        
    }
}

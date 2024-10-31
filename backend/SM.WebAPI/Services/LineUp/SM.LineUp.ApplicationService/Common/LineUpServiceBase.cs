using SM.LineUp.Infrastructure;
using Microsoft.Extensions.Logging;
using SM.LineUp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.ApplicationService.Common
{
    public class LineUpServiceBase
    {
        protected readonly ILogger _logger;
        protected readonly LineUpDbContext _dbContext;
        protected LineUpServiceBase(ILogger logger, LineUpDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
    }
}

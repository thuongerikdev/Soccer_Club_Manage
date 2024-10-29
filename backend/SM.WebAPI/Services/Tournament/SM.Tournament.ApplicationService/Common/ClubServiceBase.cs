﻿using Microsoft.Extensions.Logging;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Common
{
    public class ClubServiceBase
    {
       
            protected readonly ILogger _logger;
            protected readonly TournamentDBContext _dbContext;
            protected ClubServiceBase(ILogger logger, TournamentDBContext dbContext)
            {
                _logger = logger;
                _dbContext = dbContext;
            }

        
    }
}

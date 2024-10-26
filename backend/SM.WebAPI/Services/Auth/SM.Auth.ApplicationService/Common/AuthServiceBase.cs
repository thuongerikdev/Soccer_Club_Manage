﻿using Microsoft.Extensions.Logging;
using SM.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.ApplicationService.Common
{
    public class AuthServiceBase
    {
        protected readonly ILogger _logger;
        protected readonly AuthDbContext _dbContext;
        protected AuthServiceBase(ILogger logger, AuthDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

    }
}

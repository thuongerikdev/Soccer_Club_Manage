﻿using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Abtracts
{
    public interface IMinigameUse
    {
        public ICaculationResultStrategy chooseType(string type);
    }
}
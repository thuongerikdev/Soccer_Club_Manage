﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Player.Dtos.PlayerDto
{
    public class ReadPlayerDto
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public string PlayerImage { get; set; }
        public int ClubId { get; set; }
        public int PlayerAge { get; set; }
        public int Shirtnumber { get; set; }
        public int PlayerStatus { get; set; }
        public string leg { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
    }
}

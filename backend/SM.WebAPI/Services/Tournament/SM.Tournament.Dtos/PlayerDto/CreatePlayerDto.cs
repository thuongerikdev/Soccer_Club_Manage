using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.PlayerDto
{
    public class CreatePlayerDto
    {
         public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public string PlayerNationality { get; set; }
        public string PlayerImage { get; set; }
        public int PlayerAge { get; set; }
        public double PlayerValue { get; set; }
        public int PlayerHealth { get; set; }
        public int PlayerSkill { get; set; }
        public double PlayerSalary { get; set; }
    }
}

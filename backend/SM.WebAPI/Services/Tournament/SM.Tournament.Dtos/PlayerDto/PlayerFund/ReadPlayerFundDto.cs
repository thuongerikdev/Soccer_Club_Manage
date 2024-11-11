using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.PlayerDto.PlayerFund
{
    public  class ReadPlayerFundDto
    {
        public int PlayerFundID { get; set; }
        public int PlayerID { get; set; }
        public int ClubID { get; set; }
        public int FundActionHistoryID { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
    }
}

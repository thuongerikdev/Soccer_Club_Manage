﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory
{
    public  class UpdateActionFundDto
    {
        public int FundActionHistoryID { get; set; }
        public int FundID { get; set; }
        public decimal Amount { get; set; }
        public DateTime ActionDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string FundActionType { get; set; }
        public int PlayerID { get; set; }
    }
}

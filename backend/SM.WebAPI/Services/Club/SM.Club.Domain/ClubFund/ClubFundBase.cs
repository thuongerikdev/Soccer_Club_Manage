using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Club.Domain.ClubFund
{
    public class ClubFundBase
    {
        public int ClubFundBaseId { get; set; }
        public int ClubId { get; set; }
        public string FundName { get; set; }
        public string FundDescription { get; set; }
        public decimal FundAmount { get; set; }
        public DateTime FundDate { get; set; }
        public string FundType { get; set; }
        public string FundStatus { get; set; }
    }
}

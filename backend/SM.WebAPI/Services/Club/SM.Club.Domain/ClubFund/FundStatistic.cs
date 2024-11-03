using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Club.Domain.ClubFund
{
    public class FundStatistic
    {
        public int FundStatisticId { get; set; }
        public int ClubId { get; set; }
        public int MoneyGet { get; set; } // tiền đã thu
        public double MoneySpent { get; set; } // tiền đã chi 
        public DateTime StatisticDate { get; set; } // ngày thống kê
        public double MoneyTotal { get; set; } // tổng tiền
        public double MoneyUncollect { get; set; } //tổng tiền chưa thu 
    }
}

using SM.Constant.Database;
using SM.Tournament.Domain.Club.ClubEvent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Club.ClubFund
{

    [Table(nameof(ClubFunds), Schema = DbSchema.Tournament)]
    public  class ClubFunds
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FundID { get; set; }
        public int ClubID { get; set; }
        [MaxLength(50)]
        public string FundName { get; set; }
        public decimal Expense { get; set; }
        public decimal Debt { get; set; }
        public decimal Contribution { get; set; }
        [MaxLength(50)]
        public string FundDescription { get; set; }
        public decimal FundAmount { get; set; }
        public DateTime FundDate { get; set; }
        [MaxLength(50)]
        public string FundType { get; set; }

        public string FundStatus { get; set; }
        public void UpdateFundAmount(decimal amount)
        {
            FundAmount += amount;
        }
    }
}

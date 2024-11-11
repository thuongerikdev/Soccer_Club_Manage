using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Player
{
    [Table(nameof(PlayerFund), Schema = DbSchema.Tournament)]
    public  class PlayerFund
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerFundID { get; set; }
        public int PlayerID { get; set; }
        public int ClubID { get; set; }
        public int FundActionHistoryID { get; set; }
        public double Amount { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }

    }
}

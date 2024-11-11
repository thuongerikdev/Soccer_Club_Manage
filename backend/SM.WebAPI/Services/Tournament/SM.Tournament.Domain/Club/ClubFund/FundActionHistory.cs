using SM.Constant.Database;
using SM.Tournament.Domain.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Club.ClubFund
{
    [Table(nameof(FundActionHistory), Schema = DbSchema.Tournament)]
    public class FundActionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FundActionHistoryID { get; set; }
        public int FundID { get; set; }
        public int ClubID { get; set; }
        public decimal Amount { get; set; }
        public DateTime ActionDate { get; set; }
        public int playerMember { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string FundActionType { get; set; }
        public int PlayerID { get; set; }
    }
}

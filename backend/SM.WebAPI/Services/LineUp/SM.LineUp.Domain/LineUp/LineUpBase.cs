using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.Domain.LineUp
{
    [Table(nameof(LineUpBase))]
    public class LineUpBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineUpId { get; set; }
        public int MatchId { get; set; }
        public int ClubId { get; set; }
        [MaxLength(50)]
        public string LineUpName { get; set; }
        [MaxLength(50)]
        public string LineUpType { get; set; } // chế đọ : công khai / nội bộ / riêng tue
        [MaxLength(50)]
        public string MatchType { get; set; } // loại trận đấu : 1 - trận đấu thường, 2 - trận đấu tập huấn, 3 - trận đấu giao hữu
        [MaxLength(50)]

        public string StadiumBackGroud { get; set; } // loại sân : sân 5 người , sân 7 người 
        public DateTime CreateAt { get; set; }


    }
}

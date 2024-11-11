using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Constant.Database;

namespace SM.Tournament.Domain.LineUp
{
    [Table(nameof(LineUpBase), Schema = DbSchema.Tournament)]
    public class LineUpBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineUpID { get; set; }
        public int ClubID { get; set; }
        [MaxLength(50)]
        public string LineUpName { get; set; }
        [MaxLength(50)]
        public string LineUpType { get; set; } // chế đọ : công khai / nội bộ / riêng tue
 
        public int PlayerNumber { get; set; } // loại sân : sân 5 người , sân 7 người 
        public DateTime CreateAt { get; set; }

    }
}

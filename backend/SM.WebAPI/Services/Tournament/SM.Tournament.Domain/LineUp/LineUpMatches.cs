using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.LineUp
{
    [Table(nameof(LineUpMatches), Schema = DbSchema.Tournament)]
    public class LineUpMatches
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineUpMatchesID { get; set; }
        public int LineUpID { get; set; }
        public int MatchID { get; set; }
    }
}

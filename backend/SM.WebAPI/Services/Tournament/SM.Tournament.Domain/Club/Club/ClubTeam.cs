using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Club.Club
{
    [Table(nameof(ClubTeam) , Schema =DbSchema.Tournament)]
    public class ClubTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClubID { get; set; }
        public int UserID { get; set; }

        [MaxLength(50)]
        public string ClubName { get; set; }
        [MaxLength(50)]
        public string ClubDescription { get; set; }
        [MaxLength(50)]
        public string ClubLogo { get; set; }
        [MaxLength(50)]
        public string ClubBanner { get; set; }
        public double Budget { get; set; }
        [MaxLength(50)]
        public string ClubLevel { get; set; } //trình độ đội : vui , chuyên nghiejp , bán chuyên
        [MaxLength(50)]
        public string ClubAge { get; set; } // độ tuổi đội : 15-20 , 20-25 , 25-35
    }
}

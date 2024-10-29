using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Club
{
    [Table(nameof(ClubTeam))]
    public class ClubTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClubId { get; set; }

        [MaxLength(50)]
        public string ClubName { get; set; }
        [MaxLength(50)]
        public string ClubDescription { get; set; }
        [MaxLength(50)]
        public string ClubLogo { get; set; }
        [MaxLength(50)]
        public string ClubBanner { get; set; }
       
        public int UserId { get; set; }
        public double Budget { get; set; }
        public int CoachId { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.ClubTeamDtos
{
    public class GetClubTeamDto
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubDescription { get; set; }
        public string ClubLogo { get; set; }
        public string ClubBanner { get; set; }
        public int UserId { get; set; }
        public double Budget { get; set; }
        public int CoachId { get; set; }
    }
}

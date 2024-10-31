using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Club.Dtos.ClubTeamDtos
{
    public class UpdateClubTeamDto
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

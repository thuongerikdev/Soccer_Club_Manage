using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Club.Dtos.ClubTeamDtos
{
    public class CreateClubTeamDto
    {
        public string ClubName { get; set; }
        public string ClubDescription { get; set; }
        public int UserId { get; set; }
    }
}

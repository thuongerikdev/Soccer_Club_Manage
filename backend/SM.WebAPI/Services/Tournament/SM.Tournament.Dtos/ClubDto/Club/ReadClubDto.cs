using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.ClubDto.Club
{
    public  class ReadClubDto
    {
        public int ClubId { get; set; }
        public int UserId { get; set; }
        public string ClubName { get; set; }
        public string ClubDescription { get; set; }
        public string ClubLogo { get; set; }
        public string ClubBanner { get; set; }
        public double Budget { get; set; }
        public string ClubLevel { get; set; } //trình độ đội : vui , chuyên nghiejp , bán chuyên
        public string ClubAge { get; set; } // độ tuổi đội : 15-20 , 20-25 , 25-35
    }
}

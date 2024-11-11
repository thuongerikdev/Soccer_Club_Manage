using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.LineUpDto.LineUp
{
    public  class CreateLineUpDto
    {
        public int ClubID { get; set; }
        public string LineUpName { get; set; }
        public string LineUpType { get; set; } // chế đọ : công khai / nội bộ / riêng tue
        public int PlayerNumber { get; set; } // loại sân : sân 5 người , sân 7 người
    }
}

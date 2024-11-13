using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Club.ClubEvent
{
    public  abstract class ClubEventBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }
        [MaxLength(50)]
        public string EventName { get; set; }
        [MaxLength(50)]
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        [MaxLength(50)]
        public string EventLocation { get; set; } // địa điểm
        [MaxLength(50)]
        public string EventStatus { get; set; } //nội bộ , công khai , riêng tư
        public int membersCount { get; set; }
    }
}

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
    [Table(nameof(TeamMeetingEvent), Schema = DbSchema.Tournament)]
    public  class TeamMeetingEvent : ClubEventBase
    {
        public int ClubID { get; set; }

        [MaxLength(50)]
        public string MeetingAim { get; set; }
        [MaxLength(50)]
        public string MeetingContent { get; set; }
    }
}

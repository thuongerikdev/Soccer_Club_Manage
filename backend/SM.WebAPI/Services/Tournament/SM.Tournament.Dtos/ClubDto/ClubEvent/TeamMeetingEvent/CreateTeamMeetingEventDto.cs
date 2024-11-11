using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.ClubDto.ClubEvent.TeamMeetingEvent
{
    public  class CreateTeamMeetingEventDto
    {
        public int EventID { get; set; }
        public int ClubID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; } // địa điểm
        public string EventStatus { get; set; } //nội bộ , công khai , riêng tư
        public int membersCount { get; set; }
        public string MeetingAim { get; set; }
        public string MeetingContent { get; set; }

    }
}

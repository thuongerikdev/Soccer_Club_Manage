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
    [Table(nameof(TrainingEvent), Schema = DbSchema.Tournament)]
    public class TrainingEvent : ClubEventBase
    {
        public int ClubID { get; set; }

        [MaxLength(50)]
        public string TrainingAim { get; set; }
        [MaxLength(50)]
        public string LessonTraining { get; set; }
        [MaxLength(50)]
        public string Coach { get; set; }
        [MaxLength(50)]
        public string ProcessResult { get; set; }
    }
}

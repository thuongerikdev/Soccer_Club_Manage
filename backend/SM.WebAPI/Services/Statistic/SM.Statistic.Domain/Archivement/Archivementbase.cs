using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Statistic.Domain.Archivement
{
    public  class Archivementbase
    {
        public int ArchivementId { get; set; }
        public int ClubId { get; set; }
        public int TournamentId { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int LineUpId { get; set; }
        public string  Type { get; set; }
        public string ArchivementName { get; set; }
        public string ArchivementDescription { get; set; }
        public string ArchivementLogo { get; set; }
        public string ArchivementBanner { get; set; }
        public DateTime ArchivementDate { get; set; }
        public DateTime ArchivementTime { get; set; }
        public string ArchivementTitle { get; set; }
    }
}

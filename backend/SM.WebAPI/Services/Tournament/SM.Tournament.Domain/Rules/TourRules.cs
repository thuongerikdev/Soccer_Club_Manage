using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Rules
{
    public class TourRules
    {
        public int TourRulesId { get; set; }
        public int TournamentId { get; set; }
        public string RuleName { get; set; }
        public string RuleDescription { get; set; }
        public string RuleType { get; set; }
        public string RuleValue { get; set; }
        public string RuleStatus { get; set; }
    }
}

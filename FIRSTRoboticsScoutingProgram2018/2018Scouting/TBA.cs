using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018Scouting
{
    public class tbaBlue
    {
        public List<object> dq_team_keys { get; set; }
        public int? score { get; set; }
        public List<object> surrogate_team_keys { get; set; }
        public List<string> team_keys { get; set; }
    }

    public class tbaRed
    {
        public List<object> dq_team_keys { get; set; }
        public int? score { get; set; }
        public List<object> surrogate_team_keys { get; set; }
        public List<string> team_keys { get; set; }
    }

    public class tbaAlliances
    {
        public tbaBlue blue { get; set; }
        public tbaRed red { get; set; }
    }

    public class tbaMatchSchedule
    {
        public int? actual_time { get; set; }
        public tbaAlliances alliances { get; set; }
        public string comp_level { get; set; }
        public string event_key { get; set; }
        public string key { get; set; }
        public int match_number { get; set; }
        public int? predicted_time { get; set; }
        public int? set_number { get; set; }
        public int? time { get; set; }
        public string winning_alliance { get; set; }
    }
}

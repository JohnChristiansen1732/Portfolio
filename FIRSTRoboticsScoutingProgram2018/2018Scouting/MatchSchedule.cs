using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using Aspose.Cells;
using System.Data;
using System.Net;
using Newtonsoft.Json;

namespace _2018Scouting
{
    class MatchSchedule
    {
        public int matchNumber { set; get; }
        public int red1 { set; get; }
        public int red2 { set; get; }
        public int red3 { set; get; }
        public int blue1 { set; get; }
        public int blue2 { set; get; }
        public int blue3 { set; get; }


        public List<MatchSchedule> loadMatchSchedule(string eventCode)
        {
            //DataTable dataTable;
            List<MatchSchedule> allMatches = new List<MatchSchedule>();

            try
            {
                var url = "https://www.thebluealliance.com/api/v3/event/" + eventCode + "/matches/simple";

                var wc = new WebClient();
                wc.Headers.Add("X-TBA-Auth-Key", "btOVsObxXN4N4pTDIZZqaSH3fLQgLkjKumwYgu0HywfniUwWCy1OueaeGU1tF5MS");

                // get Raw Data
                var dataList = new List<tbaMatchSchedule>();
                var response = wc.DownloadString(url);
                dataList = JsonConvert.DeserializeObject<List<tbaMatchSchedule>>(response);

                // convert raw data into our match schedule object
                return convertToObject(dataList);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return allMatches;
        }

        public List<MatchSchedule> convertToObject(List<tbaMatchSchedule> tbaData)
        {
            var matchSchedule = new List<MatchSchedule>();

            foreach (tbaMatchSchedule match in tbaData)
            {
                if(match.comp_level == "qm")
                {
                    MatchSchedule newMatch = new MatchSchedule();
                    newMatch.matchNumber = match.match_number;
                    newMatch.red1 = Convert.ToInt32((match.alliances.red.team_keys[0]).Replace("frc", ""));
                    newMatch.red2 = Convert.ToInt32((match.alliances.red.team_keys[1]).Replace("frc", ""));
                    newMatch.red3 = Convert.ToInt32((match.alliances.red.team_keys[2]).Replace("frc", ""));
                    newMatch.blue1 = Convert.ToInt32((match.alliances.blue.team_keys[0]).Replace("frc", ""));
                    newMatch.blue2 = Convert.ToInt32((match.alliances.blue.team_keys[1]).Replace("frc", ""));
                    newMatch.blue3 = Convert.ToInt32((match.alliances.blue.team_keys[2]).Replace("frc", ""));
                    matchSchedule.Add(newMatch);
                }
            }
            return matchSchedule;
        }
    }
}

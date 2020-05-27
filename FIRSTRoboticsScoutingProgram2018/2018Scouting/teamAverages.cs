using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018Scouting
{
    class teamAverages
    {
        public int teamNumber { set; get; }
        public int matches { set; get; }
        public double aCrossLine { set; get; }
        public double aSwitch { set; get; }
        public double aScale { set; get; }
        public double aExchage { set; get; }
        public double totalAutoCubes { get; set; }
        public double tOppSwitch { set; get; }
        public double tOwnSwitch { set; get; }
        public double tScale { set; get; }
        public double tExchange { set; get; }
        public double totalCubes { set; get; }
        public double soloClimb { set; get; }
        public double helperClimb { set; get; }
        public double helpeeClimb { set; get; }
        public double failedClimb { set; get; }
        public double climbPercentage { set; get; }
        public int disabledMatches { set; get; }

        public teamAverages(List<TeamMatchData> matchData)
        {
            foreach (TeamMatchData match in matchData)
            {
                teamNumber = match.teamNumber;
                aCrossLine += match.aCrossLine;
                aSwitch += match.aSwitch;
                aScale += match.aScale;
                aExchage += match.aExchange;
                tOwnSwitch += match.tOwnSwitch;
                tOppSwitch += match.tOppSwitch;
                tScale += match.tScale;
                tExchange += match.tExchange;
                soloClimb += match.tSoloClimb;
                helpeeClimb += match.tHelpeeClimb;
                helperClimb += match.tHelperClimb;
                failedClimb += match.tFailedClimb;

                if (match.DisableTime > 0)
                {
                    disabledMatches++;
                }
            }
            matches = matchData.Count;
            climbPercentage = Math.Round((soloClimb) / (soloClimb + failedClimb), 2) * 100;
            aCrossLine = Math.Round((aCrossLine / matches), 2);
            aSwitch = Math.Round((aSwitch / matches), 2);
            aScale = Math.Round((aScale / matches), 2);
            aExchage = Math.Round((aExchage / matches), 2);
            tOwnSwitch = Math.Round((tOwnSwitch / matches), 2);
            tOppSwitch = Math.Round((tOppSwitch / matches), 2);
            tScale = Math.Round((tScale / matches), 2);
            tExchange = Math.Round((tExchange / matches), 2);
            soloClimb = Math.Round((soloClimb / matches), 2);
            helperClimb = Math.Round((helperClimb / matches), 2);
            helpeeClimb = Math.Round((helpeeClimb / matches), 2);
            failedClimb = Math.Round((failedClimb/ matches), 2);
            totalAutoCubes = aSwitch + aScale + aExchage;
            totalCubes = totalAutoCubes + tOwnSwitch + tOppSwitch + tScale + tExchange;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Aspose.Cells;
using System.Data;

namespace _2018Scouting
{
    class restoreDB
    {
        public int restoreDBFromExcel(string excelFolderFile)
        {
            DataTable dataTable;
            List<TeamMatchData> allMatches = new List<TeamMatchData>();
            using (FileStream fstream = new FileStream(excelFolderFile, FileMode.Open))
            {
                Workbook workbook = new Workbook(fstream);
                Worksheet worksheet = workbook.Worksheets["Raw Data"];
                dataTable = worksheet.Cells.ExportDataTable(0, 0, (worksheet.Cells.MaxRow + 1), (worksheet.Cells.MaxColumn + 1), true);
            }
            foreach (DataRow row in dataTable.Rows)
            {
                TeamMatchData newMatch = new TeamMatchData();
                newMatch.matchNumber = Convert.ToInt32(row["MatchNumber"]);
                newMatch.teamNumber = Convert.ToInt32(row["TeamNumber"]);
                newMatch.alliance = (row["alliance"]).ToString();
                newMatch.aCrossLine = Convert.ToInt32(row["aCrossLine"]);
                newMatch.aSwitch = Convert.ToInt32(row["aSwitch"]);
                newMatch.aScale = Convert.ToInt32(row["aScale"]);
                newMatch.aExchange = Convert.ToInt32(row["aExchange"]);
                newMatch.tOwnSwitch = Convert.ToInt32(row["tOwnSwitch"]);
                newMatch.tOppSwitch = Convert.ToInt32(row["tOppSwitch"]);
                newMatch.tScale = Convert.ToInt32(row["tScale"]);
                newMatch.tExchange = Convert.ToInt32(row["tExchange"]);
                newMatch.tSoloClimb = Convert.ToInt32(row["tSoloClimb"]);
                newMatch.tHelpeeClimb = Convert.ToInt32(row["tHelpeeClimb"]);
                newMatch.tHelperClimb = Convert.ToInt32(row["tHelperClimb"]);
                newMatch.tFailedClimb = Convert.ToInt32(row["tFailedClimb"]);
                newMatch.DisableTime = Convert.ToInt32(row["DisableTime"]);
                allMatches.Add(newMatch);
            }
            Database db = new Database();
            db.createDatabase();

            foreach (TeamMatchData match in allMatches)
            {
                db.enterTeamMatchData(match);
            }
            return allMatches.Count;
        }

    }
}

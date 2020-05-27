using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using System.Drawing;

namespace _2018Scouting
{
    class excelExport
    {
        public enum field { aCross, aSwitch, aScale, aExchange, tOwnSwitch, tOppSwitch, tScale, tExchange, soloClimb, HelperClimb, HelpeeClimb, failedClimb}
        public void exportAll()
        {
            Database sqlData = new Database();
            Workbook book = new Workbook();
            book.Worksheets.Clear();

            // Create total stats
            List<int> teamList = sqlData.getTeamList();
            List<teamAverages> allAverages = new List<teamAverages>();
            foreach (int team in teamList)
            {
                teamAverages currentTeamAverage = new teamAverages(sqlData.getTeamData(team));
                allAverages.Add(currentTeamAverage);
            }
            Worksheet averageSheet = book.Worksheets.Add("Averages");
            fillAverageSheet(averageSheet, allAverages);

            // Create 1 tab per team
            foreach (int team in teamList)
            {
                List<TeamMatchData> currentTeamStats = sqlData.getTeamData(team);
                Worksheet currentTeamSheet = book.Worksheets.Add(team.ToString());
                fillMatchSheet(currentTeamSheet, currentTeamStats);
            }

            // Create Raw Data Tab
            List<TeamMatchData> allMatches = sqlData.getAllMatches();
            Worksheet rawData = book.Worksheets.Add("Raw Data");
            fillMatchSheet(rawData, allMatches);

            StringBuilder fileName = new StringBuilder();
            fileName.Append("ScoutingData").Append("_Match").Append((allMatches.Count / 6).ToString()).Append("_").Append(DateTime.Now.ToString("yyyy-MM-dd-HH-mm")).Append(".xlsx");
            book.Save(fileName.ToString());

        }
        public void fillMatchSheet(Worksheet sheet, List<TeamMatchData> allMatches)
        {
            sheet.Cells.ImportCustomObjects(allMatches,
            new string[] { "teamNumber", "matchNumber", "alliance", "aCrossLine", "aSwitch", "aScale", "aExchange", "tOwnSwitch", "tOppSwitch", "tScale", "tExchange", "tSoloClimb", "tHelperClimb", "tHelpeeClimb", "tFailedClimb", "DisableTime"},
            true,
            0,
            0,
            allMatches.Count,
            true,
           "mm/dd/yyyy hh:mm AM/PM",
           false);
        }
        public void fillAverageSheet(Worksheet sheet, List<teamAverages> allMatches)
        {
            sheet.Cells.ImportCustomObjects(allMatches,
            new string[] { "teamNumber", "matches", "aCrossLine", "aSwitch", "aScale", "aExchage", "totalAutoCubes", "tOppSwitch", "tOwnSwitch", "tScale", "tExchange", "totalCubes", "soloClimb", "helperClimb", "helpeeClimb", "failedClimb", "climbPercentage", "disabledMatches" },
            true,
            0,
            0,
            allMatches.Count,
            true,
            "mm/dd/yyyy hh:mm AM/PM",
            false);
        }
        public void exportMatchStrategy(string matchNumber, int red1, int red2, int red3, int blue1, int blue2, int blue3)
        {
            Database sqlData = new Database();
            Workbook book = new Workbook();
            Worksheet matchSheet = book.Worksheets.Add(matchNumber);
            List<TeamMatchData> red1Stats = sqlData.getTeamData(red1);
            List<TeamMatchData> red2Stats = sqlData.getTeamData(red2);
            List<TeamMatchData> red3Stats = sqlData.getTeamData(red3);
            List<TeamMatchData> blue1Stats = sqlData.getTeamData(blue1);
            List<TeamMatchData> blue2Stats = sqlData.getTeamData(blue2);
            List<TeamMatchData> blue3Stats = sqlData.getTeamData(blue3);
            matchSheet.Cells["D1"].Value = "Match: " + matchNumber;

            matchSheet.Cells["A2"].Value = red1.ToString();
            if (red1Stats.Count > 0)
            {
                matchSheet.Cells["B2"].Value = "Match By Match";
                matchSheet.Cells["C2"].Value = "Average";

                matchSheet.Cells["A3"].Value = "Crossline";
                matchSheet.Cells["B3"].Value = getMatchByMatchString(red1Stats, field.aCross);
                matchSheet.Cells["C3"].Value = getAverage(red1Stats, field.aCross);

                matchSheet.Cells["A4"].Value = "Auto Switch";
                matchSheet.Cells["B4"].Value = getMatchByMatchString(red1Stats, field.aSwitch);
                matchSheet.Cells["C4"].Value = getAverage(red1Stats, field.aSwitch);

                matchSheet.Cells["A5"].Value = "Auto Scale";
                matchSheet.Cells["B5"].Value = getMatchByMatchString(red1Stats, field.aScale);
                matchSheet.Cells["C5"].Value = getAverage(red1Stats, field.aScale);

                matchSheet.Cells["A6"].Value = "Auto Exchange";
                matchSheet.Cells["B6"].Value = getMatchByMatchString(red1Stats, field.aExchange);
                matchSheet.Cells["C6"].Value = getAverage(red1Stats, field.aExchange);

                matchSheet.Cells["A7"].Value = "Own Switch";
                matchSheet.Cells["B7"].Value = getMatchByMatchString(red1Stats, field.tOwnSwitch);
                matchSheet.Cells["C7"].Value = getAverage(red1Stats, field.tOwnSwitch);

                matchSheet.Cells["A8"].Value = "Opp Switch";
                matchSheet.Cells["B8"].Value = getMatchByMatchString(red1Stats, field.tOppSwitch);
                matchSheet.Cells["C8"].Value = getAverage(red1Stats, field.tOppSwitch);

                matchSheet.Cells["A9"].Value = "Scale";
                matchSheet.Cells["B9"].Value = getMatchByMatchString(red1Stats, field.tScale);
                matchSheet.Cells["C9"].Value = getAverage(red1Stats, field.tScale);

                matchSheet.Cells["A10"].Value = "Exchange";
                matchSheet.Cells["B10"].Value = getMatchByMatchString(red1Stats, field.tExchange);
                matchSheet.Cells["C10"].Value = getAverage(red1Stats, field.tExchange);

                matchSheet.Cells["A11"].Value = "Solo Climb";
                matchSheet.Cells["B11"].Value = getMatchByMatchString(red1Stats, field.soloClimb);
                matchSheet.Cells["C11"].Value = getAverage(red1Stats, field.soloClimb);

                matchSheet.Cells["A12"].Value = "Helper Climb";
                matchSheet.Cells["B12"].Value = getMatchByMatchString(red1Stats, field.HelperClimb);
                matchSheet.Cells["C12"].Value = getAverage(red1Stats, field.HelperClimb);

                matchSheet.Cells["A13"].Value = "Helpee Climb";
                matchSheet.Cells["B13"].Value = getMatchByMatchString(red1Stats, field.HelpeeClimb);
                matchSheet.Cells["C13"].Value = getAverage(red1Stats, field.HelpeeClimb);

                matchSheet.Cells["A14"].Value = "Failed Climb";
                matchSheet.Cells["B14"].Value = getMatchByMatchString(red1Stats, field.failedClimb);
                matchSheet.Cells["C14"].Value = getAverage(red1Stats, field.failedClimb);

                matchSheet.Cells["A15"].Value = "Comments:";
            }

            matchSheet.Cells["A19"].Value = red2.ToString();
            if (red2Stats.Count > 0)
            {         
                matchSheet.Cells["B19"].Value = "Match By Match";
                matchSheet.Cells["C19"].Value = "Average";

                matchSheet.Cells["A20"].Value = "Crossline";
                matchSheet.Cells["B20"].Value = getMatchByMatchString(red2Stats, field.aCross);
                matchSheet.Cells["C20"].Value = getAverage(red2Stats, field.aCross);

                matchSheet.Cells["A21"].Value = "Auto Switch";
                matchSheet.Cells["B21"].Value = getMatchByMatchString(red2Stats, field.aSwitch);
                matchSheet.Cells["C21"].Value = getAverage(red2Stats, field.aSwitch);

                matchSheet.Cells["A22"].Value = "Auto Scale";
                matchSheet.Cells["B22"].Value = getMatchByMatchString(red2Stats, field.aScale);
                matchSheet.Cells["C22"].Value = getAverage(red2Stats, field.aScale);

                matchSheet.Cells["A23"].Value = "Auto Exchange";
                matchSheet.Cells["B23"].Value = getMatchByMatchString(red2Stats, field.aExchange);
                matchSheet.Cells["C23"].Value = getAverage(red2Stats, field.aExchange);

                matchSheet.Cells["A24"].Value = "Own Switch";
                matchSheet.Cells["B24"].Value = getMatchByMatchString(red2Stats, field.tOwnSwitch);
                matchSheet.Cells["C24"].Value = getAverage(red2Stats, field.tOwnSwitch);

                matchSheet.Cells["A25"].Value = "Opp Switch";
                matchSheet.Cells["B25"].Value = getMatchByMatchString(red2Stats, field.tOppSwitch);
                matchSheet.Cells["C25"].Value = getAverage(red2Stats, field.tOppSwitch);

                matchSheet.Cells["A26"].Value = "Scale";
                matchSheet.Cells["B26"].Value = getMatchByMatchString(red2Stats, field.tScale);
                matchSheet.Cells["C26"].Value = getAverage(red2Stats, field.tScale);

                matchSheet.Cells["A27"].Value = "Exchange";
                matchSheet.Cells["B27"].Value = getMatchByMatchString(red2Stats, field.tExchange);
                matchSheet.Cells["C27"].Value = getAverage(red2Stats, field.tExchange);

                matchSheet.Cells["A28"].Value = "Solo Climb";
                matchSheet.Cells["B28"].Value = getMatchByMatchString(red2Stats, field.soloClimb);
                matchSheet.Cells["C28"].Value = getAverage(red2Stats, field.soloClimb);

                matchSheet.Cells["A29"].Value = "Helper Climb";
                matchSheet.Cells["B29"].Value = getMatchByMatchString(red2Stats, field.HelperClimb);
                matchSheet.Cells["C29"].Value = getAverage(red2Stats, field.HelperClimb);

                matchSheet.Cells["A30"].Value = "Helpee Climb";
                matchSheet.Cells["B30"].Value = getMatchByMatchString(red2Stats, field.HelpeeClimb);
                matchSheet.Cells["C30"].Value = getAverage(red2Stats, field.HelpeeClimb);

                matchSheet.Cells["A31"].Value = "Failed Climb";
                matchSheet.Cells["B31"].Value = getMatchByMatchString(red2Stats, field.failedClimb);
                matchSheet.Cells["C31"].Value = getAverage(red2Stats, field.failedClimb);

                matchSheet.Cells["A32"].Value = "Comments:";
            }
            matchSheet.Cells["A36"].Value = red3.ToString();
            if (red3Stats.Count > 0)
            {
                matchSheet.Cells["B36"].Value = "Match By Match";
                matchSheet.Cells["C36"].Value = "Average";

                matchSheet.Cells["A37"].Value = "Crossline";
                matchSheet.Cells["B37"].Value = getMatchByMatchString(red3Stats, field.aCross);
                matchSheet.Cells["C37"].Value = getAverage(red3Stats, field.aCross);

                matchSheet.Cells["A38"].Value = "Auto Switch";
                matchSheet.Cells["B38"].Value = getMatchByMatchString(red3Stats, field.aSwitch);
                matchSheet.Cells["C38"].Value = getAverage(red3Stats, field.aSwitch);

                matchSheet.Cells["A39"].Value = "Auto Scale";
                matchSheet.Cells["B39"].Value = getMatchByMatchString(red3Stats, field.aScale);
                matchSheet.Cells["C39"].Value = getAverage(red3Stats, field.aScale);

                matchSheet.Cells["A40"].Value = "Auto Exchange";
                matchSheet.Cells["B40"].Value = getMatchByMatchString(red3Stats, field.aExchange);
                matchSheet.Cells["C40"].Value = getAverage(red3Stats, field.aExchange);

                matchSheet.Cells["A41"].Value = "Own Switch";
                matchSheet.Cells["B41"].Value = getMatchByMatchString(red3Stats, field.tOwnSwitch);
                matchSheet.Cells["C41"].Value = getAverage(red3Stats, field.tOwnSwitch);

                matchSheet.Cells["A42"].Value = "Opp Switch";
                matchSheet.Cells["B42"].Value = getMatchByMatchString(red3Stats, field.tOppSwitch);
                matchSheet.Cells["C42"].Value = getAverage(red3Stats, field.tOppSwitch);

                matchSheet.Cells["A43"].Value = "Scale";
                matchSheet.Cells["B43"].Value = getMatchByMatchString(red3Stats, field.tScale);
                matchSheet.Cells["C43"].Value = getAverage(red3Stats, field.tScale);

                matchSheet.Cells["A44"].Value = "Exchange";
                matchSheet.Cells["B44"].Value = getMatchByMatchString(red3Stats, field.tExchange);
                matchSheet.Cells["C44"].Value = getAverage(red3Stats, field.tExchange);

                matchSheet.Cells["A45"].Value = "Solo Climb";
                matchSheet.Cells["B45"].Value = getMatchByMatchString(red3Stats, field.soloClimb);
                matchSheet.Cells["C45"].Value = getAverage(red3Stats, field.soloClimb);

                matchSheet.Cells["A46"].Value = "Helper Climb";
                matchSheet.Cells["B46"].Value = getMatchByMatchString(red3Stats, field.HelperClimb);
                matchSheet.Cells["C46"].Value = getAverage(red3Stats, field.HelperClimb);

                matchSheet.Cells["A47"].Value = "Helpee Climb";
                matchSheet.Cells["B47"].Value = getMatchByMatchString(red3Stats, field.HelpeeClimb);
                matchSheet.Cells["C47"].Value = getAverage(red3Stats, field.HelpeeClimb);

                matchSheet.Cells["A48"].Value = "Failed Climb";
                matchSheet.Cells["B48"].Value = getMatchByMatchString(red3Stats, field.failedClimb);
                matchSheet.Cells["C48"].Value = getAverage(red3Stats, field.failedClimb);

                matchSheet.Cells["A49"].Value = "Comments:";
            }
            
            matchSheet.Cells["E2"].Value = blue1.ToString();
            if (blue1Stats.Count > 0)
            {
                matchSheet.Cells["F2"].Value = "Match By Match";
                matchSheet.Cells["G2"].Value = "Average";

                matchSheet.Cells["E3"].Value = "Crossline";
                matchSheet.Cells["F3"].Value = getMatchByMatchString(blue1Stats, field.aCross);
                matchSheet.Cells["G3"].Value = getAverage(blue1Stats, field.aCross);

                matchSheet.Cells["E4"].Value = "Auto Switch";
                matchSheet.Cells["F4"].Value = getMatchByMatchString(blue1Stats, field.aSwitch);
                matchSheet.Cells["G4"].Value = getAverage(blue1Stats, field.aSwitch);

                matchSheet.Cells["E5"].Value = "Auto Scale";
                matchSheet.Cells["F5"].Value = getMatchByMatchString(blue1Stats, field.aScale);
                matchSheet.Cells["G5"].Value = getAverage(blue1Stats, field.aScale);

                matchSheet.Cells["E6"].Value = "Auto Exchange";
                matchSheet.Cells["F6"].Value = getMatchByMatchString(blue1Stats, field.aExchange);
                matchSheet.Cells["G6"].Value = getAverage(blue1Stats, field.aExchange);

                matchSheet.Cells["E7"].Value = "Own Switch";
                matchSheet.Cells["F7"].Value = getMatchByMatchString(blue1Stats, field.tOwnSwitch);
                matchSheet.Cells["G7"].Value = getAverage(blue1Stats, field.tOwnSwitch);

                matchSheet.Cells["E8"].Value = "Opp Switch";
                matchSheet.Cells["F8"].Value = getMatchByMatchString(blue1Stats, field.tOppSwitch);
                matchSheet.Cells["G8"].Value = getAverage(blue1Stats, field.tOppSwitch);

                matchSheet.Cells["E9"].Value = "Scale";
                matchSheet.Cells["F9"].Value = getMatchByMatchString(blue1Stats, field.tScale);
                matchSheet.Cells["G9"].Value = getAverage(blue1Stats, field.tScale);

                matchSheet.Cells["E10"].Value = "Exchange";
                matchSheet.Cells["F10"].Value = getMatchByMatchString(blue1Stats, field.tExchange);
                matchSheet.Cells["G10"].Value = getAverage(blue1Stats, field.tExchange);

                matchSheet.Cells["E11"].Value = "Solo Climb";
                matchSheet.Cells["F11"].Value = getMatchByMatchString(blue1Stats, field.soloClimb);
                matchSheet.Cells["G11"].Value = getAverage(blue1Stats, field.soloClimb);

                matchSheet.Cells["E12"].Value = "Helper Climb";
                matchSheet.Cells["F12"].Value = getMatchByMatchString(blue1Stats, field.HelperClimb);
                matchSheet.Cells["G12"].Value = getAverage(blue1Stats, field.HelperClimb);

                matchSheet.Cells["E13"].Value = "Helpee Climb";
                matchSheet.Cells["F13"].Value = getMatchByMatchString(blue1Stats, field.HelpeeClimb);
                matchSheet.Cells["G13"].Value = getAverage(blue1Stats, field.HelpeeClimb);

                matchSheet.Cells["E14"].Value = "Failed Climb";
                matchSheet.Cells["F14"].Value = getMatchByMatchString(blue1Stats, field.failedClimb);
                matchSheet.Cells["G14"].Value = getAverage(blue1Stats, field.failedClimb);

                matchSheet.Cells["E15"].Value = "Comments:";
            }
            matchSheet.Cells["E19"].Value = blue2.ToString();
            if (blue2Stats.Count > 0)
            {
                matchSheet.Cells["F19"].Value = "Match By Match";
                matchSheet.Cells["G19"].Value = "Average";

                matchSheet.Cells["E20"].Value = "Crossline";
                matchSheet.Cells["F20"].Value = getMatchByMatchString(blue2Stats, field.aCross);
                matchSheet.Cells["G20"].Value = getAverage(blue2Stats, field.aCross);

                matchSheet.Cells["E21"].Value = "Auto Switch";
                matchSheet.Cells["F21"].Value = getMatchByMatchString(blue2Stats, field.aSwitch);
                matchSheet.Cells["G21"].Value = getAverage(blue2Stats, field.aSwitch);

                matchSheet.Cells["E22"].Value = "Auto Scale";
                matchSheet.Cells["F22"].Value = getMatchByMatchString(blue2Stats, field.aScale);
                matchSheet.Cells["G22"].Value = getAverage(blue2Stats, field.aScale);

                matchSheet.Cells["E23"].Value = "Auto Exchange";
                matchSheet.Cells["F23"].Value = getMatchByMatchString(blue2Stats, field.aExchange);
                matchSheet.Cells["G23"].Value = getAverage(blue2Stats, field.aExchange);

                matchSheet.Cells["E24"].Value = "Own Switch";
                matchSheet.Cells["F24"].Value = getMatchByMatchString(blue2Stats, field.tOwnSwitch);
                matchSheet.Cells["G24"].Value = getAverage(blue2Stats, field.tOwnSwitch);

                matchSheet.Cells["E25"].Value = "Opp Switch";
                matchSheet.Cells["F25"].Value = getMatchByMatchString(blue2Stats, field.tOppSwitch);
                matchSheet.Cells["G25"].Value = getAverage(blue2Stats, field.tOppSwitch);

                matchSheet.Cells["E26"].Value = "Scale";
                matchSheet.Cells["F26"].Value = getMatchByMatchString(blue2Stats, field.tScale);
                matchSheet.Cells["G26"].Value = getAverage(blue2Stats, field.tScale);

                matchSheet.Cells["E27"].Value = "Exchange";
                matchSheet.Cells["F27"].Value = getMatchByMatchString(blue2Stats, field.tExchange);
                matchSheet.Cells["G27"].Value = getAverage(blue2Stats, field.tExchange);

                matchSheet.Cells["E28"].Value = "Solo Climb";
                matchSheet.Cells["F28"].Value = getMatchByMatchString(blue2Stats, field.soloClimb);
                matchSheet.Cells["G28"].Value = getAverage(blue2Stats, field.soloClimb);

                matchSheet.Cells["E29"].Value = "Helper Climb";
                matchSheet.Cells["F29"].Value = getMatchByMatchString(blue2Stats, field.HelperClimb);
                matchSheet.Cells["G29"].Value = getAverage(blue2Stats, field.HelperClimb);

                matchSheet.Cells["E30"].Value = "Helpee Climb";
                matchSheet.Cells["F30"].Value = getMatchByMatchString(blue2Stats, field.HelpeeClimb);
                matchSheet.Cells["G30"].Value = getAverage(blue2Stats, field.HelpeeClimb);

                matchSheet.Cells["E31"].Value = "Failed Climb";
                matchSheet.Cells["F31"].Value = getMatchByMatchString(blue2Stats, field.failedClimb);
                matchSheet.Cells["G31"].Value = getAverage(blue2Stats, field.failedClimb);

                matchSheet.Cells["E32"].Value = "Comments:";
            }

            matchSheet.Cells["E36"].Value = blue3.ToString();
            if (blue3Stats.Count > 0)
            {
                matchSheet.Cells["F36"].Value = "Match By Match";
                matchSheet.Cells["G36"].Value = "Average";

                matchSheet.Cells["E37"].Value = "Crossline";
                matchSheet.Cells["F37"].Value = getMatchByMatchString(blue3Stats, field.aCross);
                matchSheet.Cells["G37"].Value = getAverage(blue3Stats, field.aCross);

                matchSheet.Cells["E38"].Value = "Auto Switch";
                matchSheet.Cells["F38"].Value = getMatchByMatchString(blue3Stats, field.aSwitch);
                matchSheet.Cells["G38"].Value = getAverage(blue3Stats, field.aSwitch);

                matchSheet.Cells["E39"].Value = "Auto Scale";
                matchSheet.Cells["F39"].Value = getMatchByMatchString(blue3Stats, field.aScale);
                matchSheet.Cells["G39"].Value = getAverage(blue3Stats, field.aScale);

                matchSheet.Cells["E40"].Value = "Auto Exchange";
                matchSheet.Cells["F40"].Value = getMatchByMatchString(blue3Stats, field.aExchange);
                matchSheet.Cells["G40"].Value = getAverage(blue3Stats, field.aExchange);

                matchSheet.Cells["E41"].Value = "Own Switch";
                matchSheet.Cells["F41"].Value = getMatchByMatchString(blue3Stats, field.tOwnSwitch);
                matchSheet.Cells["G41"].Value = getAverage(blue3Stats, field.tOwnSwitch);

                matchSheet.Cells["E42"].Value = "Opp Switch";
                matchSheet.Cells["F42"].Value = getMatchByMatchString(blue3Stats, field.tOppSwitch);
                matchSheet.Cells["G42"].Value = getAverage(blue3Stats, field.tOppSwitch);

                matchSheet.Cells["E43"].Value = "Scale";
                matchSheet.Cells["F43"].Value = getMatchByMatchString(blue3Stats, field.tScale);
                matchSheet.Cells["G43"].Value = getAverage(blue3Stats, field.tScale);

                matchSheet.Cells["E44"].Value = "Exchange";
                matchSheet.Cells["F44"].Value = getMatchByMatchString(blue3Stats, field.tExchange);
                matchSheet.Cells["G44"].Value = getAverage(blue3Stats, field.tExchange);

                matchSheet.Cells["E45"].Value = "Solo Climb";
                matchSheet.Cells["F45"].Value = getMatchByMatchString(blue3Stats, field.soloClimb);
                matchSheet.Cells["G45"].Value = getAverage(blue3Stats, field.soloClimb);

                matchSheet.Cells["E46"].Value = "Helper Climb";
                matchSheet.Cells["F46"].Value = getMatchByMatchString(blue3Stats, field.HelperClimb);
                matchSheet.Cells["G46"].Value = getAverage(blue3Stats, field.HelperClimb);

                matchSheet.Cells["E47"].Value = "Helpee Climb";
                matchSheet.Cells["F47"].Value = getMatchByMatchString(blue3Stats, field.HelpeeClimb);
                matchSheet.Cells["G47"].Value = getAverage(blue3Stats, field.HelpeeClimb);

                matchSheet.Cells["E48"].Value = "Failed Climb";
                matchSheet.Cells["F48"].Value = getMatchByMatchString(blue3Stats, field.failedClimb);
                matchSheet.Cells["G48"].Value = getAverage(blue3Stats, field.failedClimb);

                matchSheet.Cells["E49"].Value = "Comments:";
            }

            matchSheet.Cells["A52"].Value = "Match Strategy";

            matchSheet.AutoFitColumns();

            styleSheet(matchSheet);
            
            StringBuilder fileName = new StringBuilder();
            fileName.Append("MatchSheet_").Append(matchNumber).Append("_").Append(DateTime.Now.ToString("yyyy-MM-dd-HH-mm")).Append(".xlsx");
            book.Save(fileName.ToString());
        }
        public string getMatchByMatchString (List<TeamMatchData> teamStats, field field)
        {
            StringBuilder matchByMatch = new StringBuilder();
            if (field == field.aCross)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.aCrossLine).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.aSwitch)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.aSwitch).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.aScale)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.aScale).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.aExchange)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.aExchange).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.tOwnSwitch)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.tOwnSwitch).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.tOppSwitch)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.tOppSwitch).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.tOwnSwitch)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.tOwnSwitch).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.tScale)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.tScale).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.tExchange)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.tExchange).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.soloClimb)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.tSoloClimb).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.HelperClimb)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.tHelperClimb).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.HelpeeClimb)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.tHelpeeClimb).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else if (field == field.failedClimb)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    matchByMatch.Append((match.tFailedClimb).ToString());
                    matchByMatch.Append(", ");
                }
                matchByMatch.Length = matchByMatch.Length - 2;
                return matchByMatch.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public string getAverage (List<TeamMatchData> teamStats, field field)
        {
            double average = 0;
            if (field == field.aCross)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.aCrossLine;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.aSwitch)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.aSwitch;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.aScale)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.aScale;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.aExchange)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.aExchange;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.tOwnSwitch)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.tOwnSwitch;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.tOppSwitch)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.tOppSwitch;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.tScale)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.tScale;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.tExchange)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.tExchange;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.soloClimb)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.tSoloClimb;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.HelperClimb)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.tHelperClimb;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.HelpeeClimb)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.tHelpeeClimb;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.HelperClimb)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.tHelperClimb;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else if (field == field.failedClimb)
            {
                foreach (TeamMatchData match in teamStats)
                {
                    average += match.tFailedClimb;
                }
                average = Math.Round((average / teamStats.Count), 2);
                return average.ToString();
            }
            else
            {
                return average.ToString();
            }
        }
        public void styleSheet(Worksheet sheet)
        {
            boldCell(sheet.Cells["A2"]);
            boldCell(sheet.Cells["A19"]);
            boldCell(sheet.Cells["A36"]);
            boldCell(sheet.Cells["E2"]);
            boldCell(sheet.Cells["E19"]);
            boldCell(sheet.Cells["E36"]);

            createBorder(sheet.Cells.CreateRange("A2:C18"));
            createBorder(sheet.Cells.CreateRange("A19:C35"));
            createBorder(sheet.Cells.CreateRange("A36:C51"));
            createBorder(sheet.Cells.CreateRange("E2:G18"));
            createBorder(sheet.Cells.CreateRange("E19:G35"));
            createBorder(sheet.Cells.CreateRange("E36:G51"));

            Cells cells = sheet.Cells;
            for (int i = 3; i <= 6; i++)
            {
                Cell currentCell = sheet.Cells[string.Concat("C", i)];
                colorLessThanOne(currentCell);
                currentCell = sheet.Cells[string.Concat("G", i)];
                colorLessThanOne(currentCell);
            }
            for (int i = 11; i <= 13; i++)
            {
                Cell currentCell = sheet.Cells[string.Concat("C", i)];
                colorLessThanOne(currentCell);
                currentCell = sheet.Cells[string.Concat("G", i)];
                colorLessThanOne(currentCell);
            }
            for (int i = 20; i <= 23; i++)
            {
                Cell currentCell = sheet.Cells[string.Concat("C", i)];
                colorLessThanOne(currentCell);
                currentCell = sheet.Cells[string.Concat("G", i)];
                colorLessThanOne(currentCell);
            }
            for (int i = 28; i <= 30; i++)
            {
                Cell currentCell = sheet.Cells[string.Concat("C", i)];
                colorLessThanOne(currentCell);
                currentCell = sheet.Cells[string.Concat("G", i)];
                colorLessThanOne(currentCell);
            }
            for (int i = 37; i <= 40; i++)
            {
                Cell currentCell = sheet.Cells[string.Concat("C", i)];
                colorLessThanOne(currentCell);
                currentCell = sheet.Cells[string.Concat("G", i)];
                colorLessThanOne(currentCell);
            }
            for (int i = 45; i <= 47; i++)
            {
                Cell currentCell = sheet.Cells[string.Concat("C", i)];
                colorLessThanOne(currentCell);
                currentCell = sheet.Cells[string.Concat("G", i)];
                colorLessThanOne(currentCell);
            }
            for (int i = 7; i <= 10; i++)
            {
                Cell currentCell = sheet.Cells[string.Concat("C", i)];
                colorGreaterThanOne(currentCell);
                currentCell = sheet.Cells[string.Concat("G", i)];
                colorGreaterThanOne(currentCell);
            }
            for (int i = 24; i <= 27; i++)
            {
                Cell currentCell = sheet.Cells[string.Concat("C", i)];
                colorGreaterThanOne(currentCell);
                currentCell = sheet.Cells[string.Concat("G", i)];
                colorGreaterThanOne(currentCell);
            }
            for (int i = 41; i <= 44; i++)
            {
                Cell currentCell = sheet.Cells[string.Concat("C", i)];
                colorGreaterThanOne(currentCell);
                currentCell = sheet.Cells[string.Concat("G", i)];
                colorGreaterThanOne(currentCell);
            }
        }
        public void boldCell (Cell c)
        {
            Style s = c.GetStyle();
            s.Font.IsBold = true;
            c.SetStyle(s);
        }
        public void colorLessThanOne (Cell currentCell)
        {
            if (Convert.ToDouble(currentCell.Value) >= 1)
            {
                darkGray(currentCell);
            }
            else if (Convert.ToDouble(currentCell.Value) >= .6)
            {
                gray(currentCell);
            }
            else if (Convert.ToDouble(currentCell.Value) >= .3)
            {
                lightgray(currentCell);
            }
        }
        public void colorGreaterThanOne(Cell currentCell)
        {

            if (Convert.ToDouble(currentCell.Value) >= 5)
            {
                darkGray(currentCell);
            }
            else if (Convert.ToDouble(currentCell.Value) >= 3)
            {
                gray(currentCell);
            }
            else if (Convert.ToDouble(currentCell.Value) >= 1)
            {
                lightgray(currentCell);
            }
        }

        public void darkGray(Cell c)
        {
            Style s = c.GetStyle();
            s.Pattern = BackgroundType.Gray50;
            c.SetStyle(s);
        }
        public void gray(Cell c)
        {
            Style s = c.GetStyle();
            s.Pattern = BackgroundType.Gray25;
            c.SetStyle(s);
        }
        public void lightgray(Cell c)
        {
            Style s = c.GetStyle();
            s.Pattern = BackgroundType.Gray6;
            c.SetStyle(s);
        }
        public void createBorder (Range rb)
        {
            rb.SetOutlineBorders(CellBorderType.Medium, Color.Black);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Net;
using Newtonsoft.Json;

namespace _2018Scouting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void submitMatch_onClick(object sender, EventArgs e)
        {
            try
            {
                // get validation from config
                validationValues validValues = new validationValues();
                validValues.maxTeamNumber = Convert.ToInt32(validation_maxTeamNumber.Text);
                validValues.maxASwitch = Convert.ToInt32(validation_maxASwitch.Text);
                validValues.maxAScale = Convert.ToInt32(validation_maxAScale.Text);
                validValues.maxAExchange = Convert.ToInt32(validation_maxAExchange.Text);
                validValues.maxTSwitch = Convert.ToInt32(validation_maxTSwitch.Text);
                validValues.maxTScale = Convert.ToInt32(validation_maxTScale.Text);
                validValues.maxTExchange = Convert.ToInt32(validation_maxTExchange.Text);
                validValues.maxHelperClimb = Convert.ToInt32(validation_MaxHelperClimb.Text);
                // Fill each robots stats
                TeamMatchData red1 = fillRed1();
                if (!red1.validate(validValues))
                {
                    System.Windows.Forms.MessageBox.Show("red1 not valid: " + red1.errorMessage);
                    return;
                }
                TeamMatchData red2 = fillRed2();
                if (!red2.validate(validValues))
                {
                    System.Windows.Forms.MessageBox.Show("red2 not valid: " + red2.errorMessage);
                    return;
                }
                TeamMatchData red3 = fillRed3();
                if (!red3.validate(validValues))
                {
                    System.Windows.Forms.MessageBox.Show("red3 not valid: " + red3.errorMessage);
                    return;
                }
                TeamMatchData blue1 = fillBlue1();
                if (!blue1.validate(validValues))
                {
                    System.Windows.Forms.MessageBox.Show("blue1 not valid: " + blue1.errorMessage);
                    return;
                }
                TeamMatchData blue2 = fillBlue2();
                if (!blue2.validate(validValues))
                {
                    System.Windows.Forms.MessageBox.Show("blue2 not valid: " + blue2.errorMessage);
                    return;
                }
                TeamMatchData blue3 = fillBlue3();
                if (!blue3.validate(validValues))
                {
                    System.Windows.Forms.MessageBox.Show("blue3 not valid: " + blue3.errorMessage);
                    return;
                }

                // enter into DB
                Database dbase = new Database();
                dbase.createDatabase();
                dbase.deleteMatch(Convert.ToInt32(matchNumber.Text));
                if (red1_NoData.Checked == false)
                {
                    dbase.enterTeamMatchData(red1);
                }
                if (red2_NoData.Checked == false)
                {
                    dbase.enterTeamMatchData(red2);
                }
                if (red3_NoData.Checked == false)
                {
                    dbase.enterTeamMatchData(red3);
                }
                if (blue1_NoData.Checked == false)
                {
                    dbase.enterTeamMatchData(blue1);
                }
                if (blue2_NoData.Checked == false)
                {
                    dbase.enterTeamMatchData(blue2);
                }
                if (blue3_NoData.Checked == false)
                {
                    dbase.enterTeamMatchData(blue3);
                }
                System.Windows.Forms.MessageBox.Show("Match Entered");

                // Save previous match number, clear form, add next match
                int prevMatch = Convert.ToInt32(matchNumber.Text);
                UncheckAll(this);
                zeroAll(this);
                matchNumber.Text = (prevMatch + 1).ToString();
                if (useMatchScheduleCheck.Checked)
                {
                    Database sqData = new Database();
                    MatchSchedule match = sqData.getMatchFromSchedule(Convert.ToInt32(matchNumber.Text));
                    matchNumber.Text = match.matchNumber.ToString();
                    red1_TeamNum.Text = match.red1.ToString();
                    red2_TeamNum.Text = match.red2.ToString();
                    red3_TeamNum.Text = match.red3.ToString();
                    blue1_TeamNum.Text = match.blue1.ToString();
                    blue2_TeamNum.Text = match.blue2.ToString();
                    blue3_TeamNum.Text = match.blue3.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        private void exportToExcel_onClick(object sender, EventArgs e)
        {
            try
            {
                excelExport excel = new excelExport();
                excel.exportAll();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            System.Windows.Forms.MessageBox.Show("Export Complete");
        }
        private void openNewMatch(object sender, EventArgs e)
        {
            try
            {
                if (useMatchScheduleCheck.Checked)
                {
                    Database sqData = new Database();
                    MatchSchedule match = sqData.getMatchFromSchedule(Convert.ToInt32(matchNumber.Text));
                    UncheckAll(this);
                    zeroAll(this);
                    matchNumber.Text = match.matchNumber.ToString();
                    red1_TeamNum.Text = match.red1.ToString();
                    red2_TeamNum.Text = match.red2.ToString();
                    red3_TeamNum.Text = match.red3.ToString();
                    blue1_TeamNum.Text = match.blue1.ToString();
                    blue2_TeamNum.Text = match.blue2.ToString();
                    blue3_TeamNum.Text = match.blue3.ToString();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Allow for match schedule in config");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        private TeamMatchData fillRed1()
        {
            TeamMatchData red1 = new TeamMatchData();
            red1.matchNumber = Convert.ToInt32(matchNumber.Text);
            red1.alliance = "red1";
            red1.teamNumber = Convert.ToInt32(red1_TeamNum.Text);
            if (red1_crossline.Checked)
            {
                red1.aCrossLine = 1;
            }
            else
            {
                red1.aCrossLine = 0;
            }
            red1.aSwitch = Convert.ToInt32(red1_aswitch.Text);
            red1.aScale = Convert.ToInt32(red1_ascale.Text);
            red1.aExchange = Convert.ToInt32(red1_aexchange.Text);
            red1.tOppSwitch = Convert.ToInt32(red1_toppswitch.Text);
            red1.tOwnSwitch = Convert.ToInt32(red1_townswitch.Text);
            red1.tScale = Convert.ToInt32(red1_tscale.Text);
            red1.tExchange = Convert.ToInt32(red1_texchange.Text);
            red1.tHelperClimb = Convert.ToInt32(red1_helper.Text);
            if (red1_solo.Checked)
            {
                red1.tSoloClimb = 1;
            }
            else
            {
                red1.tSoloClimb = 0;
            }

            if (red1_helpee.Checked)
            {
                red1.tHelpeeClimb = 1;
            }
            else
            {
                red1.tHelpeeClimb = 0;
            }

            if (red1_failed.Checked)
            {
                red1.tFailedClimb = 1;
            }
            else
            {
                red1.tFailedClimb = 0;
            }
            red1.DisableTime = Convert.ToInt32(red1_Disable.Text);
            return red1;
        }
        private TeamMatchData fillRed2()
        {
            TeamMatchData red2 = new TeamMatchData();
            red2.matchNumber = Convert.ToInt32(matchNumber.Text);
            red2.alliance = "red2";
            red2.teamNumber = Convert.ToInt32(red2_TeamNum.Text);
            if (red2_crossline.Checked)
            {
                red2.aCrossLine = 1;
            }
            else
            {
                red2.aCrossLine = 0;
            }
            red2.aSwitch = Convert.ToInt32(red2_aswitch.Text);
            red2.aScale = Convert.ToInt32(red2_ascale.Text);
            red2.aExchange = Convert.ToInt32(red2_aexchange.Text);
            red2.tOppSwitch = Convert.ToInt32(red2_toppswitch.Text);
            red2.tOwnSwitch = Convert.ToInt32(red2_townswitch.Text);
            red2.tScale = Convert.ToInt32(red2_tscale.Text);
            red2.tExchange = Convert.ToInt32(red2_texchange.Text);
            red2.tHelperClimb = Convert.ToInt32(red2_helper.Text);
            if (red2_solo.Checked)
            {
                red2.tSoloClimb = 1;
            }
            else
            {
                red2.tSoloClimb = 0;
            }

            if (red2_helpee.Checked)
            {
                red2.tHelpeeClimb = 1;
            }
            else
            {
                red2.tHelpeeClimb = 0;
            }

            if (red2_failed.Checked)
            {
                red2.tFailedClimb = 1;
            }
            else
            {
                red2.tFailedClimb = 0;
            }
            red2.DisableTime = Convert.ToInt32(red2_Disable.Text);
            return red2;
        }
        private TeamMatchData fillRed3()
        {
            TeamMatchData red3 = new TeamMatchData();
            red3.matchNumber = Convert.ToInt32(matchNumber.Text);
            red3.alliance = "red3";
            red3.teamNumber = Convert.ToInt32(red3_TeamNum.Text);
            if (red3_crossline.Checked)
            {
                red3.aCrossLine = 1;
            }
            else
            {
                red3.aCrossLine = 0;
            }
            red3.aSwitch = Convert.ToInt32(red3_aswitch.Text);
            red3.aScale = Convert.ToInt32(red3_ascale.Text);
            red3.aExchange = Convert.ToInt32(red3_aexchange.Text);
            red3.tOppSwitch = Convert.ToInt32(red3_toppswitch.Text);
            red3.tOwnSwitch = Convert.ToInt32(red3_townswitch.Text);
            red3.tScale = Convert.ToInt32(red3_tscale.Text);
            red3.tExchange = Convert.ToInt32(red3_texchange.Text);
            red3.tHelperClimb = Convert.ToInt32(red3_helper.Text);
            if (red3_solo.Checked)
            {
                red3.tSoloClimb = 1;
            }
            else
            {
                red3.tSoloClimb = 0;
            }

            if (red3_helpee.Checked)
            {
                red3.tHelpeeClimb = 1;
            }
            else
            {
                red3.tHelpeeClimb = 0;
            }

            if (red3_failed.Checked)
            {
                red3.tFailedClimb = 1;
            }
            else
            {
                red3.tFailedClimb = 0;
            }
            red3.DisableTime = Convert.ToInt32(red3_Disable.Text);
            return red3;
        }
        private TeamMatchData fillBlue1()
        {
            TeamMatchData blue1 = new TeamMatchData();
            blue1.matchNumber = Convert.ToInt32(matchNumber.Text);
            blue1.alliance = "blue1";
            blue1.teamNumber = Convert.ToInt32(blue1_TeamNum.Text);
            if (blue1_crossline.Checked)
            {
                blue1.aCrossLine = 1;
            }
            else
            {
                blue1.aCrossLine = 0;
            }
            blue1.aSwitch = Convert.ToInt32(blue1_aswitch.Text);
            blue1.aScale = Convert.ToInt32(blue1_ascale.Text);
            blue1.aExchange = Convert.ToInt32(blue1_aexchange.Text);
            blue1.tOppSwitch = Convert.ToInt32(blue1_toppswitch.Text);
            blue1.tOwnSwitch = Convert.ToInt32(blue1_townswitch.Text);
            blue1.tScale = Convert.ToInt32(blue1_tscale.Text);
            blue1.tExchange = Convert.ToInt32(blue1_texchange.Text);
            blue1.tHelperClimb = Convert.ToInt32(blue1_helper.Text);
            if (blue1_solo.Checked)
            {
                blue1.tSoloClimb = 1;
            }
            else
            {
                blue1.tSoloClimb = 0;
            }

            if (blue1_helpee.Checked)
            {
                blue1.tHelpeeClimb = 1;
            }
            else
            {
                blue1.tHelpeeClimb = 0;
            }

            if (blue1_failed.Checked)
            {
                blue1.tFailedClimb = 1;
            }
            else
            {
                blue1.tFailedClimb = 0;
            }
            blue1.DisableTime = Convert.ToInt32(blue1_Disable.Text);
            return blue1;
        }
        private TeamMatchData fillBlue2()
        {
            TeamMatchData blue2 = new TeamMatchData();
            blue2.matchNumber = Convert.ToInt32(matchNumber.Text);
            blue2.alliance = "blue2";
            blue2.teamNumber = Convert.ToInt32(blue2_TeamNum.Text);
            if (blue2_crossline.Checked)
            {
                blue2.aCrossLine = 1;
            }
            else
            {
                blue2.aCrossLine = 0;
            }
            blue2.aSwitch = Convert.ToInt32(blue2_aswitch.Text);
            blue2.aScale = Convert.ToInt32(blue2_ascale.Text);
            blue2.aExchange = Convert.ToInt32(blue2_aexchange.Text);
            blue2.tOppSwitch = Convert.ToInt32(blue2_toppswitch.Text);
            blue2.tOwnSwitch = Convert.ToInt32(blue2_townswitch.Text);
            blue2.tScale = Convert.ToInt32(blue2_tscale.Text);
            blue2.tExchange = Convert.ToInt32(blue2_texchange.Text);
            blue2.tHelperClimb = Convert.ToInt32(blue2_helper.Text);
            if (blue2_solo.Checked)
            {
                blue2.tSoloClimb = 1;
            }
            else
            {
                blue2.tSoloClimb = 0;
            }

            if (blue2_helpee.Checked)
            {
                blue2.tHelpeeClimb = 1;
            }
            else
            {
                blue2.tHelpeeClimb = 0;
            }

            if (blue2_failed.Checked)
            {
                blue2.tFailedClimb = 1;
            }
            else
            {
                blue2.tFailedClimb = 0;
            }
            blue2.DisableTime = Convert.ToInt32(blue2_Disable.Text);
            return blue2;
        }
        private TeamMatchData fillBlue3()
        {
            TeamMatchData blue3 = new TeamMatchData();
            blue3.matchNumber = Convert.ToInt32(matchNumber.Text);
            blue3.alliance = "blue3";
            blue3.teamNumber = Convert.ToInt32(blue3_TeamNum.Text);
            if (blue3_crossline.Checked)
            {
                blue3.aCrossLine = 1;
            }
            else
            {
                blue3.aCrossLine = 0;
            }
            blue3.aSwitch = Convert.ToInt32(blue3_aswitch.Text);
            blue3.aScale = Convert.ToInt32(blue3_ascale.Text);
            blue3.aExchange = Convert.ToInt32(blue3_aexchange.Text);
            blue3.tOppSwitch = Convert.ToInt32(blue3_toppswitch.Text);
            blue3.tOwnSwitch = Convert.ToInt32(blue3_townswitch.Text);
            blue3.tScale = Convert.ToInt32(blue3_tscale.Text);
            blue3.tExchange = Convert.ToInt32(blue3_texchange.Text);
            blue3.tHelperClimb = Convert.ToInt32(blue3_helper.Text);
            if (blue3_solo.Checked)
            {
                blue3.tSoloClimb = 1;
            }
            else
            {
                blue3.tSoloClimb = 0;
            }

            if (blue3_helpee.Checked)
            {
                blue3.tHelpeeClimb = 1;
            }
            else
            {
                blue3.tHelpeeClimb = 0;
            }

            if (blue3_failed.Checked)
            {
                blue3.tFailedClimb = 1;
            }
            else
            {
                blue3.tFailedClimb = 0;
            }
            blue3.DisableTime = Convert.ToInt32(blue3_Disable.Text);
            return blue3;
        }
        private void UncheckAll(Control ctrl)
        {
            CheckBox chkBox = ctrl as CheckBox;
            if (chkBox == null)
            {
                foreach (Control child in ctrl.Controls)
                {
                    UncheckAll(child);
                }
            }
            else
            {
                if (chkBox != useMatchScheduleCheck)
                {
                    chkBox.Checked = false;
                }
            }
        }
        private void zeroAll(Control ctrl)
        {
            TextBox txtBox = ctrl as TextBox;
            if (txtBox == null)
            {
                foreach (Control child in ctrl.Controls)
                {
                    if (child != validationBox)
                    {
                        zeroAll(child);
                    }
                }
            }
            else
            {
                txtBox.Text = "0";
            }
        }

        private void importMatchSchedule(object sender, EventArgs e)
        {
            int numEntered = 0;
            try
            {
                MatchSchedule schedule = new MatchSchedule();
                Database sqlDB = new Database();
                sqlDB.createMatchSchedule();
                numEntered = sqlDB.enterMatchSchedule(schedule.loadMatchSchedule(tbaEventCode.Text.ToString()));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            System.Windows.Forms.MessageBox.Show(numEntered + " matches entered");
        }
        private void restoreDB(object sender, EventArgs e)
        {
            try
            {
                restoreDB restore = new restoreDB();
                int numEntered = restore.restoreDBFromExcel(restoreDBFolderFile.Text);
                System.Windows.Forms.MessageBox.Show((numEntered / 6) + " matches entered");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        // to do: implement
        private void normalizeStats(object sender, EventArgs e)
        {
            //    try
            //    {
            //        var url = string.Empty;
            //        if (Wisconsin_Radio.Checked)
            //        {
            //            url = ("https://www.thebluealliance.com/api/v2/event/2017cars/matches");
            //        }


            //        normalizeFuelScores normalize = new normalizeFuelScores();
            //        normalize.normalize(url);
            //        System.Windows.Forms.MessageBox.Show("FMS Stats Added");
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Windows.Forms.MessageBox.Show(ex.Message);
            //    }

            //}

        }

        private void export_OpenMatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (useMatchScheduleCheck.Checked)
                {
                    Database sqData = new Database();
                    MatchSchedule match = sqData.getMatchFromSchedule(Convert.ToInt32(export_MatchNumber.Text));
                    export_Red1.Text = match.red1.ToString();
                    export_Red2.Text = match.red2.ToString();
                    export_Red3.Text = match.red3.ToString();
                    export_Blue1.Text = match.blue1.ToString();
                    export_Blue2.Text = match.blue2.ToString();
                    export_Blue3.Text = match.blue3.ToString();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Allow for match schedule in config");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void exportMatchStrategy_Click(object sender, EventArgs e)
        {
            try
            {
                excelExport excel = new excelExport();
                excel.exportMatchStrategy(export_MatchNumber.Text, Convert.ToInt32(export_Red1.Text), Convert.ToInt32(export_Red2.Text), Convert.ToInt32(export_Red3.Text), Convert.ToInt32(export_Blue1.Text), Convert.ToInt32(export_Blue2.Text), Convert.ToInt32(export_Blue3.Text));
                System.Windows.Forms.MessageBox.Show("Export Complete");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
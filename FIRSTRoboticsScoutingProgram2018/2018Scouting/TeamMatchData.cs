using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018Scouting
{
    class TeamMatchData
    {
        public int teamNumber { get; set; }
        public int matchNumber { get; set; }
        public string alliance { get; set; }
        public int aCrossLine { get; set; }
        public int aSwitch { get; set; }
        public int aScale { get; set; }
        public int aExchange { get; set; }
        public int tOwnSwitch { get; set; }
        public int tOppSwitch { get; set; }
        public int tScale { get; set; }
        public int tExchange { get; set; }
        public int tSoloClimb { get; set; }
        public int tHelperClimb { get; set; }
        public int tHelpeeClimb { get; set; }
        public int tFailedClimb { get; set; }
        public int DisableTime { get; set; }
        public string errorMessage { get; set; }

        // To Do: fill out
        public bool validate(validationValues validValues)
        {
            if (teamNumber < 1 || teamNumber > validValues.maxTeamNumber)
            {
                errorMessage = "Check Team Number";
                return false;
            }
            if (aSwitch < 0 || aSwitch > validValues.maxASwitch)
            {
                errorMessage = "Check Auto Switch";
                return false;
            }
            if (aScale < 0 || aScale > validValues.maxAScale)
            {
                errorMessage = "Check Auto Scale";
                return false;
            }
            if (aExchange < 0 || aScale > validValues.maxAScale)
            {
                errorMessage = "Check Auto Exchange";
                return false;
            }
            if (tOwnSwitch < 0 || tOwnSwitch > validValues.maxTSwitch)
            {
                errorMessage = "Check Tele Own Switch";
                return false;
            }
            if (tOppSwitch < 0 || tOppSwitch > validValues.maxTSwitch)
            {
                errorMessage = "Check Tele Opp Switch";
                return false;
            }
            if (tScale < 0 || tScale > validValues.maxTScale)
            {
                errorMessage = "Check Tele Scale";
                return false;
            }
            if (tExchange < 0 || tExchange > validValues.maxTExchange)
            {
                errorMessage = "Check Tele Exchange";
                return false;
            }
            if (tHelperClimb < 0 || tHelperClimb > validValues.maxHelperClimb)
            {
                errorMessage = "Check Helper Climb";
                return false;
            }
            if (DisableTime < 0 || DisableTime > 150)
            {
                errorMessage = "Check Disabled Time";
                return false;
            }
            return true;
        }

    }
}

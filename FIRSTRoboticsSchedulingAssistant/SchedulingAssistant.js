function onOpen(e) {
  createUI();
}

function getScheduleFromTBA(event) {
  var options = {
     "async": true,
     "crossDomain": true,
     "method" : "GET",
     "headers" : {
       "X-TBA-Auth-Key" : "btOVsObxXN4N4pTDIZZqaSH3fLQgLkjKumwYgu0HywfniUwWCy1OueaeGU1tF5MS"
     }
   };
    var response = UrlFetchApp.fetch("https://www.thebluealliance.com/api/v3/event/" + event + "/matches/simple", options);
    var json = response.getContentText();
    var data = JSON.parse(json);
  
    var matchSchedule = new Array();
    
  

    if (data)
    {
      for (i=0; i < data.length; i++)
      {
        if (data[i]["comp_level"] == "qm")
        {
          var currentMatch = {matchNumber: 0, red1:"0", red2:"0", red3:"0", blue1:"0", blue2:"0", blue3:"0"};
          currentMatch.matchNumber = data[i]["match_number"];
          var redAlliance = data[i]["alliances"]["red"]["team_keys"].toString();
          currentMatch.red1 = redAlliance.split(',')[0].replace('frc','');
          currentMatch.red2 = redAlliance.split(',')[1].replace('frc','');
          currentMatch.red3 = redAlliance.split(',')[2].replace('frc','');
          
          var blueAlliance = data[i]["alliances"]["blue"]["team_keys"].toString();
          currentMatch.blue1 = blueAlliance.split(',')[0].replace('frc','');
          currentMatch.blue2 = blueAlliance.split(',')[1].replace('frc','');
          currentMatch.blue3 = blueAlliance.split(',')[2].replace('frc','');
          matchSchedule.push(currentMatch);
        }
      }
    }
 matchSchedule.sort(function(a, b){return a.matchNumber - b.matchNumber;});
  return matchSchedule;
}

function createUI()
{
    var ui = SpreadsheetApp.getUi();
  ui.createMenu('Create Schedule')
      .addItem('Superscout Schedule', 'createSuperScoutSchedule')
      .addItem('Match Stratgy Schedule', 'createMatchStrategySchedule')
      .addToUi();
}


function createSuperScoutSchedule(){
  
  var ss = SpreadsheetApp.getActiveSpreadsheet();
  var superScoutSchedule = ss.getSheetByName('Super Scout Schedule');
  superScoutSchedule.getRange('D1:J200').clearContent();
  var matchSchedule = getScheduleFromTBA(superScoutSchedule.getRange('B2').getValue());
  
  var numTeams = superScoutSchedule.getLastRow();
  var superScoutRange = superScoutSchedule.getRange(2, 1, numTeams, 1)
  var SuperTeams = superScoutRange.getValues();
  
  for ( i = 0; i < matchSchedule.length; i++)
  {
    superScoutSchedule.getRange(i+2, 3).setValue(matchSchedule[i].matchNumber);
    var currentMatchColumnn = 4;
    for (j = 0; j < numTeams - 1; j++)
    {
      if (SuperTeams[j] == matchSchedule[i].red1)
      {
        superScoutSchedule.getRange(i+2, currentMatchColumnn).setValue(SuperTeams[j]);
        currentMatchColumnn++;
      }    
      if (SuperTeams[j] == matchSchedule[i].red2)
      {
        superScoutSchedule.getRange(i+2, currentMatchColumnn).setValue(SuperTeams[j]);
        currentMatchColumnn++;
      } 
      if (SuperTeams[j] == matchSchedule[i].red3)
      {
        superScoutSchedule.getRange(i+2, currentMatchColumnn).setValue(SuperTeams[j]);
        currentMatchColumnn++;
      } 
       if (SuperTeams[j] == matchSchedule[i].blue1)
      {
        superScoutSchedule.getRange(i+2, currentMatchColumnn).setValue(SuperTeams[j]);
        currentMatchColumnn++;
      } 
      if (SuperTeams[j] == matchSchedule[i].blue2)
      {
        superScoutSchedule.getRange(i+2, currentMatchColumnn).setValue(SuperTeams[j]);
        currentMatchColumnn++;
      } 
      if (SuperTeams[j] == matchSchedule[i].blue3)
      {
        superScoutSchedule.getRange(i+2, currentMatchColumnn).setValue(SuperTeams[j]);
        currentMatchColumnn++;
      } 
    }
  }
  
}
function createMatchStrategySchedule(){
  var ss = SpreadsheetApp.getActiveSpreadsheet();
  var superScoutSchedule = ss.getSheetByName('Match Strategy Schedule');
  superScoutSchedule.getRange('C2:J200').clearContent();
  var matchSchedule = getScheduleFromTBA(superScoutSchedule.getRange("B2").getValue());
  var myTeam = superScoutSchedule.getRange("A2").getValue();
  
  var teamsMatches = new Array();
  var strategySchedule = new Array(matchSchedule.length);
  for (i=0; i < matchSchedule.length; i++)
  {
    strategySchedule[i] = new Array();
  }
  
  for ( i = 0; i < matchSchedule.length; i++)
  {
      if (myTeam == matchSchedule[i].red1)
      {
        teamsMatches.push(matchSchedule[i]);
      }    
      if (myTeam == matchSchedule[i].red2)
      {
        teamsMatches.push(matchSchedule[i]);
      } 
      if (myTeam == matchSchedule[i].red3)
      {
        teamsMatches.push(matchSchedule[i]);
      } 
      if (myTeam == matchSchedule[i].blue1)
      {
        teamsMatches.push(matchSchedule[i]);
      } 
      if (myTeam == matchSchedule[i].blue2)
      {
        teamsMatches.push(matchSchedule[i]);
      } 
      if (myTeam == matchSchedule[i].blue3)
      {
        teamsMatches.push(matchSchedule[i]);
      } 
  }
  
  var scoutSchedule = new Array(matchSchedule.length);
  for (i=0; i < teamsMatches.length; i++)
  {
    for (j=0; j < teamsMatches[i].matchNumber; j++)
    {
      if (teamsMatches[i].matchNumber != matchSchedule[j].matchNumber)
      {
        if (checkifMatchContainsTeam(teamsMatches[i].red1, matchSchedule[j]) && teamsMatches[i].red1 != myTeam)
        {
          strategySchedule[j].push(teamsMatches[i].red1);
        }
        if (checkifMatchContainsTeam(teamsMatches[i].red2, matchSchedule[j]) && teamsMatches[i].red2 != myTeam)
        {
          strategySchedule[j].push(teamsMatches[i].red2);
        }
        if (checkifMatchContainsTeam(teamsMatches[i].red3, matchSchedule[j]) && teamsMatches[i].red3 != myTeam)
        {
          strategySchedule[j].push(teamsMatches[i].red3);
        }
        if (checkifMatchContainsTeam(teamsMatches[i].blue1, matchSchedule[j]) && teamsMatches[i].blue1 != myTeam)
        {
          strategySchedule[j].push(teamsMatches[i].blue1);
        }
        if (checkifMatchContainsTeam(teamsMatches[i].blue2, matchSchedule[j]) && teamsMatches[i].blue2 != myTeam)
        {
          strategySchedule[j].push(teamsMatches[i].blue2);
        }
         if (checkifMatchContainsTeam(teamsMatches[i].blue3, matchSchedule[j]) && teamsMatches[i].blue3 != myTeam)
        {
          strategySchedule[j].push(teamsMatches[i].blue3);
        }
      }
    }
  }
  for (i=0; i < matchSchedule.length; i++)
  {
      superScoutSchedule.getRange(i+2, 3).setValue(i+1);
      for (j=0; j < strategySchedule[i].length; j++)
      {
        strategySchedule[i] = removeDuplicates(strategySchedule[i]);
        superScoutSchedule.getRange(i+2, j + 4).setValue(strategySchedule[i][j]);
      }
  }
  
  function checkifMatchContainsTeam (team, match)
  {
      if (team == match.red1)
      {
        return true;
      }  
      if (team == match.red2)
      {
        return true;
      }   
      if (team == match.red3)
      {
        return true;
      }   
      if (team == match.blue1)
      {
        return true;
      }   
      if (team == match.blue2)
      {
        return true;
      } 
       if (team == match.blue3)
      {
        return true;
      }   
      return false;
  }
function removeDuplicates(arr){
    var unique_array = [];
    for(var i = 0;i < arr.length; i++){
        if(unique_array.indexOf(arr[i]) == -1){
            unique_array.push(arr[i]);
        }
    }
    return unique_array;
}
  
};
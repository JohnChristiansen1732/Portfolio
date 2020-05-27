using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2018Scouting
{
    class Database
    {
        public void createDatabase()
        {
            if (!File.Exists("databaseFile.db1"))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile("databaseFile.db1");
            }

            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [Scouting] (
                          [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [Team] int NOT NULL,
                          [MatchNumber] int NOT NULL,
                          [Alliance] NVARCHAR(2048) NOT NULL,
                          [aCrossLine] int NOT NULL,
                          [aSwitch] int NOT NULL,
                          [aScale] int NOT NULL,
                          [aExchange] int NOT NULL,
                          [tOwnSwitch] int NOT NULL,
                          [tOppSwitch] int NOT NULL,
                          [tScale] int NOT NULL,
                          [tExchange] int NOT NULL,
                          [tSoloClimb] int NOT NULL,
                          [tHelperclimb] int NOT NULL,
                          [tHelpeeclimb] int NOT NULL,
                          [tFailedClimb] int NOT NULL,
                          [DisableTime] int NOT NULL
                          )";
            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=databaseFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();

                    com.CommandText = createTableQuery;
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void enterTeamMatchData(TeamMatchData data)
        {
            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=databaseFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();
                    StringBuilder sqlInsert = new StringBuilder();
                    sqlInsert.Append("INSERT INTO Scouting (Team, MatchNumber, Alliance, aCrossLine, aSwitch, aScale, aExchange, tOwnSwitch, tOppSwitch, tScale, tExchange, tSoloClimb, tHelperclimb, tHelpeeclimb, tFailedClimb, DisableTime) Values (");
                    sqlInsert.Append(data.teamNumber);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.matchNumber);
                    sqlInsert.Append(", ");
                    sqlInsert.Append("'");
                    sqlInsert.Append(data.alliance);
                    sqlInsert.Append("'");
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.aCrossLine);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.aSwitch);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.aScale);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.aExchange);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.tOwnSwitch);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.tOppSwitch);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.tScale);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.tExchange);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.tSoloClimb);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.tHelperClimb);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.tHelpeeClimb);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.tFailedClimb);
                    sqlInsert.Append(", ");
                    sqlInsert.Append(data.DisableTime);
                    sqlInsert.Append(") ");

                    com.CommandText = sqlInsert.ToString();
                    com.ExecuteNonQuery();

                    con.Close();        // Close the connection to the database
                }
            }
        }
        // to do: Add FMS stats
        public void addFMSFuel(TeamMatchData data)
        {
            //using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=databaseFile.db1"))
            //{
            //    using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
            //    {
            //        con.Open();
            //        StringBuilder sqlInsert = new StringBuilder();
            //        sqlInsert.Append("UPDATE Scouting SET ");
            //        sqlInsert.Append("aFuelLow_FMS= ");
            //        sqlInsert.Append(data.aFuelLow_FMS);
            //        sqlInsert.Append(", ");

            //        sqlInsert.Append("aFuelHigh_FMS= ");
            //        sqlInsert.Append(data.aFuelHigh_FMS);
            //        sqlInsert.Append(", ");

            //        sqlInsert.Append("tFuelLow_FMS= ");
            //        sqlInsert.Append(data.tFuelLow_FMS);
            //        sqlInsert.Append(", ");

            //        sqlInsert.Append("tFuelHigh_FMS= ");
            //        sqlInsert.Append(data.tFuelHigh_FMS);

            //        sqlInsert.Append(" WHERE ");
            //        sqlInsert.Append("Team=");
            //        sqlInsert.Append(data.teamNumber);
            //        sqlInsert.Append(" AND MatchNumber= ");
            //        sqlInsert.Append(data.matchNumber);
            //        sqlInsert.Append(";");


            //        com.CommandText = sqlInsert.ToString();
            //        com.ExecuteNonQuery();

            //        con.Close();        // Close the connection to the database
            //    }
            //}
        }
        public List<TeamMatchData> getTeamData(int team)
        {
            StringBuilder sqlQuery = new StringBuilder();
            List<TeamMatchData> allMatches = new List<TeamMatchData>();

            sqlQuery.Append("Select * FROM Scouting WHERE Team=");
            sqlQuery.Append(team);
            sqlQuery.Append(" ORDER by MatchNumber ASC");

            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=databaseFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();
                    com.CommandText = sqlQuery.ToString();
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            addNewTeamMatchData(allMatches, reader);
                        }
                    }
                    con.Close();        // Close the connection to the database
                }
            }
            return allMatches;
        }
        public TeamMatchData getTeamDatabyMatchAlliance(int matchNumber, string alliance)
        {
            StringBuilder sqlQuery = new StringBuilder();
            List<TeamMatchData> allMatches = new List<TeamMatchData>();

            sqlQuery.Append("Select * FROM Scouting WHERE MatchNumber=");
            sqlQuery.Append(matchNumber);
            sqlQuery.Append(" AND Alliance='");

            sqlQuery.Append(alliance);
            sqlQuery.Append("' ORDER by MatchNumber ASC");

            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=databaseFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();
                    com.CommandText = sqlQuery.ToString();
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            addNewTeamMatchData(allMatches, reader);
                        }
                    }
                    con.Close();        // Close the connection to the database
                }
            }
            return allMatches[0];
        }
        public int getMatchCount(int matchNumber)
        {
            StringBuilder sqlQuery = new StringBuilder();
            int matchCount = 0;

            sqlQuery.Append("Select * FROM Scouting WHERE MatchNumber=");
            sqlQuery.Append(matchNumber);

            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=databaseFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();
                    com.CommandText = sqlQuery.ToString();
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            matchCount++;
                        }
                    }
                    con.Close();        // Close the connection to the database
                }
            }
            return matchCount;
        }
        public List<int> getTeamList()
        {
            List<int> teamList = new List<int>();

            string sqlQuery = "Select DISTINCT Team FROM Scouting ORDER By Team ASC";

            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("datasource=databaseFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();

                    com.CommandText = sqlQuery;
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int newTeam = Convert.ToInt32(reader["Team"]);
                            teamList.Add(newTeam);
                        }
                    }
                    con.Close();
                }
            }
            return teamList;
        }
        public List<TeamMatchData> getAllMatches()
        {
            List<TeamMatchData> allMatches = new List<TeamMatchData>();

            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("datasource=databaseFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();

                    com.CommandText = "Select * from Scouting";
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            addNewTeamMatchData(allMatches, reader);
                        }
                    }
                    con.Close();
                }
            }
            return allMatches;
        }
        public void deleteMatch(int match)
        {
            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=databaseFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();

                    com.CommandText = "DELETE from Scouting WHERE MatchNumber= '" + match + "'";
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void createMatchSchedule()
        {
            if (!File.Exists("matchScheduleFile.db1"))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile("matchScheduleFile.db1");
            }

            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [MatchSchedule] (
                          [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [MatchNumber] int NOT NULL,
                          [Red1] int NOT NULL,
                          [Red2] int NOT NULL,
                          [Red3] int NOT NULL,
                          [Blue1] int NOT NULL,
                          [Blue2] int NOT NULL,
                          [Blue3] int NOT NULL
                          )";
            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=matchScheduleFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();

                    com.CommandText = createTableQuery;
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public int enterMatchSchedule(List<MatchSchedule> schedule)
        {
            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=matchScheduleFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();
                    foreach (MatchSchedule match in schedule)
                    {
                        StringBuilder sqlInsert = new StringBuilder();
                        sqlInsert.Append("INSERT INTO MatchSchedule (MatchNumber, Red1, Red2, Red3, Blue1, Blue2, Blue3) Values (");
                        sqlInsert.Append(match.matchNumber);
                        sqlInsert.Append(", ");
                        sqlInsert.Append(match.red1);
                        sqlInsert.Append(", ");
                        sqlInsert.Append("'");
                        sqlInsert.Append(match.red2);
                        sqlInsert.Append("'");
                        sqlInsert.Append(", ");
                        sqlInsert.Append(match.red3);
                        sqlInsert.Append(", ");
                        sqlInsert.Append(match.blue1);
                        sqlInsert.Append(", ");
                        sqlInsert.Append(match.blue2);
                        sqlInsert.Append(", ");
                        sqlInsert.Append(match.blue3);
                        sqlInsert.Append(") ");

                        com.CommandText = sqlInsert.ToString();
                        com.ExecuteNonQuery();
                    }

                    con.Close();        // Close the connection to the database
                }
            }
            return schedule.Count;
        }
        public MatchSchedule getMatchFromSchedule(int match)
        {
            string query = "SELECT * FROM MatchSchedule WHERE MatchNumber='" + match + "'";
            MatchSchedule matchFromSchedule = new MatchSchedule();
            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=matchScheduleFile.db1"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();
                    com.CommandText = query;
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("MatchNumber"))]))
                            {
                                matchFromSchedule.matchNumber = Convert.ToInt32(reader["MatchNumber"]);
                            }
                            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("Red1"))]))
                            {
                                matchFromSchedule.red1 = Convert.ToInt32(reader["Red1"]);
                            }
                            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("Red2"))]))
                            {
                                matchFromSchedule.red2 = Convert.ToInt32(reader["Red2"]);
                            }
                            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("Red3"))]))
                            {
                                matchFromSchedule.red3 = Convert.ToInt32(reader["Red3"]);
                            }
                            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("Blue1"))]))
                            {
                                matchFromSchedule.blue1 = Convert.ToInt32(reader["Blue1"]);
                            }
                            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("Blue2"))]))
                            {
                                matchFromSchedule.blue2 = Convert.ToInt32(reader["Blue2"]);
                            }
                            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("Blue3"))]))
                            {
                                matchFromSchedule.blue3 = Convert.ToInt32(reader["Blue3"]);
                            }
                        }
                    }
                    con.Close();        // Close the connection to the database
                }
            }
            return matchFromSchedule;
        }

        internal void addNewTeamMatchData(List<TeamMatchData> allMatches, System.Data.SQLite.SQLiteDataReader reader)
        {
            TeamMatchData newTeamMatch = new TeamMatchData();
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("Team"))]))
            {
                newTeamMatch.teamNumber = Convert.ToInt32(reader["Team"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("MatchNumber"))]))
            {
                newTeamMatch.matchNumber = Convert.ToInt32(reader["MatchNumber"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("Alliance"))]))
            {
                newTeamMatch.alliance = reader["Alliance"].ToString();
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("aCrossLine"))]))
            {
                newTeamMatch.aCrossLine = Convert.ToInt32(reader["aCrossLine"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("aSwitch"))]))
            {
                newTeamMatch.aSwitch = Convert.ToInt32(reader["aSwitch"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("aScale"))]))
            {
                newTeamMatch.aScale = Convert.ToInt32(reader["aScale"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("aExchange"))]))
            {
                newTeamMatch.aExchange = Convert.ToInt32(reader["aExchange"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("tOwnSwitch"))]))
            {
                newTeamMatch.tOwnSwitch = Convert.ToInt32(reader["tOwnSwitch"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("tOppSwitch"))]))
            {
                newTeamMatch.tOppSwitch = Convert.ToInt32(reader["tOppSwitch"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("tScale"))]))
            {
                newTeamMatch.tScale = Convert.ToInt32(reader["tScale"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("tExchange"))]))
            {
                newTeamMatch.tExchange = Convert.ToInt32(reader["tExchange"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("tSoloClimb"))]))
            {
                newTeamMatch.tSoloClimb = Convert.ToInt32(reader["tSoloClimb"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("tHelperclimb"))]))
            {
                newTeamMatch.tHelperClimb = Convert.ToInt32(reader["tHelperclimb"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("tHelpeeclimb"))]))
            {
                newTeamMatch.tHelpeeClimb = Convert.ToInt32(reader["tHelpeeclimb"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("tFailedClimb"))]))
            {
                newTeamMatch.tFailedClimb = Convert.ToInt32(reader["tFailedClimb"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("DisableTime"))]))
            {
                newTeamMatch.DisableTime = Convert.ToInt32(reader["DisableTime"]);
            }
            allMatches.Add(newTeamMatch);
        }
    }
}

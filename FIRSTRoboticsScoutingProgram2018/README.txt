This program, written in C#, is to collect, store, calculate analytics and display reports for the 2018 FIRST Robotics Game.  FIRST is a robotics competition where high school students work alongside profression mentors to design, build and compete in a sports-style game.  This program is used during the competitions by the Strategy Subteam to collect information on other robots during matches.

Data Collection:
-- Calls The Blue Alliance Web API to collect match schedule, stored within SQL database, to pre-populate team numbers for each match
-- Data Validation: Ensures each field is entered and fall within acceptable ranges (configurable by 'config' tab)

Data Storage / Analytics:
-- Match data stored within SQL database
-- Calculates averages, standard deviations and trends

Reports:
-- Exports reports to excel
-- Match Strategy: Creates 1 page summary of 6 team in match, highlighting each averages and trends and displays projected final score.
-- Final Stats: Creates 1 page summary with each teams's averages.  1 Tab per team displaying match by match data.
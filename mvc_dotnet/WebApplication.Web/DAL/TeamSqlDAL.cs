using SportsClubOrganizer.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SportsClubOrganizer.Web.DAL
{
    public class TeamSqlDAL
    {
        private readonly string connectionString;

        private readonly string GetTeamsByLeagueSQL = "SELECT * from Teams WHERE League = @LeagueName Order by Name; ";
        private readonly string GetLeagueByUserSQL = "SELECT League from Teams JOIN users on users.id = TEAMS.UserID WHERE users.id = @id; ";
        private readonly string GetAllTeamsSQL = "SELECT * from Teams; ";
        private readonly string GetTeamByUserIDSQL = "SELECT * from Teams WHERE UserID = @UserID; ";
        private readonly string GetTeamByTeamIDSQL = "SELECT * from Teams WHERE id = @TeamID; ";
        private readonly string GetDatesByTeamIDSQL = "SELECT * from Teams JOIN EventDates on EventDates.TeamID = TEAMS.id  WHERE TeamID = @TeamID; ";
        private readonly string InsertTeamSQL = "SELECT MAX(id) as max from users";
        private readonly string InsertIntoTeamsSQL = "INSERT INTO Teams (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES (@Name, @League, @Org, @PrimaryVenue, @SecondaryVenue, @userID); ";
        private readonly string UpdateTeamSQL = "UPDATE Teams SET Name = @Name, League = @League, Org = @Org, PrimaryVenue = @Pvenue, SecondaryVenue = @SVenue WHERE UserID = @UserID; ";
        private readonly string AddTravelDateToDBSQL = "INSERT into EventDates(TeamID, Date, Home) VALUES (@TeamID, @Date, @Home); ";
        private readonly string AddHomeDateToDBSQL = "INSERT into EventDates(TeamID, Date, Home) VALUES (@TeamID, @Date, @Home); ";
        private readonly string GetHomeDatesSQL = "Select * from EventDates WHERE TeamID = @TeamID and Home = 1; ";
        private readonly string GetTravelDatesSQL = "Select * from EventDates WHERE TeamID = @TeamID and Home = 0; ";
        private readonly string CreateLeagueSQL = "INSERT INTO Leagues (leagueName, org, sport) VALUES (@LeagueName, @Org, @Sport); ";
        private readonly string GetAllLeaguesSQL = "SELECT * from Leagues; ";
        private readonly string AdminUpdateTeamSQL = "UPDATE TEAMS SET Name = @Name, League = @League, Org = @Org, PrimaryVenue = @Pvenue, SecondaryVenue = @SVenue WHERE id = @TeamID; ";
        private readonly string GetScheduleByTeamSQL = "SELECT * FROM Schedule where homeTeam = @teamName  OR awayTeam = @teamName; ";
        private readonly string GetRosterSQL = "SELECT rosterID, firstName, lastName, email, phone, Teams.Name FROM Roster JOIN Teams on Roster.teamID = Teams.id WHERE Teams.id = @teamID";
        private readonly string GetPlayerSQL = "SELECT rosterID, firstName, lastName, email, phone, Teams.Name FROM Roster JOIN Teams on Roster.teamID = Teams.id WHERE rosterID = @rosterID;";
        private readonly string GetAllTeamsOrderByHomeAvailabilitySQL = "Select * from EventDates WHERE TeamID = @TeamID and Home = 1; ";
        private readonly string GetAllTeamsOrderByTimesPlayedSQL = "Select Count(*) as count FROM Games where(teamID1 = @userTeamID or teamID2 = @userTeamID)  and(teamID1 = @teamID or teamID2 = @teamID); ";
        private readonly string GetAllTeamsOrderByTravelAvailabilitySQL = "Select * from EventDates WHERE TeamID = @TeamID and Home = 0; ";

        public TeamSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Player> GetRoster(int teamID)
        {
            List<Player> roster = new List<Player>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetRosterSQL, conn);
                    cmd.Parameters.AddWithValue("@teamID", teamID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        roster.Add(MapRowToPlayer(reader));
                    }
                }

                return roster;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public Player GetPlayerByID(int ID)
        {
            Player player = new Player();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetPlayerSQL, conn);
                    cmd.Parameters.AddWithValue("@rosterID", ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        player = MapRowToPlayer(reader);
                    }
                }

                return player;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Team> GetTeamsByLeague(string League)
        {
            List<Team> teams = new List<Team>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetTeamsByLeagueSQL, conn);
                    cmd.Parameters.AddWithValue("@LeagueName", League);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        teams.Add(MapRowToTeam(reader));
                    }
                }

                return teams;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public string GetLeagueByUser(User user)
        {
            string league = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetLeagueByUserSQL, conn);
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        league = Convert.ToString(reader["League"]);
                    }
                }

                return league;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Team> GetAllTeams()
        {
            List<Team> teams = new List<Team>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetAllTeamsSQL, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        teams.Add(MapRowToTeam(reader));
                    }
                }

                return teams;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Team> GetAllTeamsOrderByHomeAvailability()
        {
            List<Team> teams = new List<Team>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetAllTeamsOrderByHomeAvailabilitySQL, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        teams.Add(MapRowToTeam(reader));
                    }
                }

                return teams;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Team> GetAllTeamsOrderByTravelAvailability()
        {
            List<Team> teams = new List<Team>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetAllTeamsOrderByTravelAvailabilitySQL, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        teams.Add(MapRowToTeam(reader));
                    }
                }

                return teams;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int GetCountOfTimesPlayed(User user, int TeamID)
        {
            int count = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetAllTeamsOrderByTimesPlayedSQL, conn);
                    cmd.Parameters.AddWithValue("@userTeamID", user.TeamID);
                    cmd.Parameters.AddWithValue("@teamID", TeamID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader["count"]);
                    }
                }

                return count;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public Team GetTeamByUserID(int? userID)
        {
            Team team = new Team();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetTeamByUserIDSQL, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        team = (MapRowToTeam(reader));
                    }
                }

                return team;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public Team GetTeamByTeamID(string TeamID)
        {
            Team team = new Team();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetTeamByTeamIDSQL, conn);
                    cmd.Parameters.AddWithValue("@TeamID", TeamID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        team = (MapRowToTeam(reader));
                        team.UserID = Convert.ToInt32(reader["UserID"]);
                    }
                }

                return team;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public Team GetDatesByTeamID(User user)
        {
            Team team = new Team();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetDatesByTeamIDSQL, conn);
                    cmd.Parameters.AddWithValue("@TeamID", user.UserTeam.TeamID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        team = (MapRowToTeam(reader));

                        if (Convert.ToInt32(reader["Home"]) == 1)
                        {
                            team.HomeDates.Add(Convert.ToDateTime(reader["Date"]));
                        }
                        else if (Convert.ToInt32(reader["Home"]) == 0)
                        {
                            team.TravelDates.Add(Convert.ToDateTime(reader["Date"]));
                        }
                        else
                        {
                            DateTime date = new DateTime(2020, 01, 01);
                            team.TravelDates.Add(date);
                        }
                    }
                }

                return team;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void AddTravelDateToDB(DateTime? TravelDate, User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(AddTravelDateToDBSQL, conn);
                    cmd.Parameters.AddWithValue("@TeamID", 1);
                    cmd.Parameters.AddWithValue("@Date", TravelDate);
                    cmd.Parameters.AddWithValue("@Home", 0);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void AddHomeDateToDB(DateTime? HomeDate, User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(AddHomeDateToDBSQL, conn);
                    cmd.Parameters.AddWithValue("@TeamID", 1);
                    cmd.Parameters.AddWithValue("@Date", HomeDate);
                    cmd.Parameters.AddWithValue("@Home", 1);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<DateTime?> GetHomeDates(string TeamID)
        {
            List<DateTime?> dates = new List<DateTime?>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetHomeDatesSQL, conn);
                    cmd.Parameters.AddWithValue("@TeamID", TeamID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["Date"]);

                        dates.Add(date);
                    }
                }

                return dates;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<DateTime?> GetTravelDates(string TeamID)
        {
            List<DateTime?> dates = new List<DateTime?>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetTravelDatesSQL, conn);
                    cmd.Parameters.AddWithValue("@TeamID", TeamID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["Date"]);

                        dates.Add(date);
                    }
                }

                return dates;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Game> GetScheduleByTeam(Team team)
        {
            List<Game> games = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetScheduleByTeamSQL, conn);
                    cmd.Parameters.AddWithValue("@teamName", team.Name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Game game = new Game
                        {
                            AwayTeam = Convert.ToString(reader["awayTeam"]),
                            HomeTeam = Convert.ToString(reader["homeTeam"]),
                            Venue = Convert.ToString(reader["venue"]),
                            Date = Convert.ToDateTime(reader["date"])
                        };

                        games.Add(game);
                    }
                }

                return games;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void InsertTeam(Team team)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(InsertTeamSQL, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    int id = 0;
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader["max"]);
                    }
                    reader.Close();

                    SqlCommand comd = new SqlCommand(InsertIntoTeamsSQL, conn);
                    comd.Parameters.AddWithValue("@Name", team.Name);
                    comd.Parameters.AddWithValue("@League", team.League);
                    comd.Parameters.AddWithValue("@Org", team.Org);
                    comd.Parameters.AddWithValue("@PrimaryVenue", team.PrimaryVenue);
                    comd.Parameters.AddWithValue("@SecondaryVenue", team.SecondaryVenue);
                    comd.Parameters.AddWithValue("@userID", id);

                    comd.ExecuteNonQuery();

                    //INSERT INTO EventDates(TeamID, Date, Home) VALUES(36, '2020-09-12', 0);

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void UpdateTeam(Team team)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(UpdateTeamSQL, conn);
                    cmd.Parameters.AddWithValue("@Name", team.Name);
                    cmd.Parameters.AddWithValue("@League", team.League);
                    cmd.Parameters.AddWithValue("@Org", team.Org);
                    cmd.Parameters.AddWithValue("@PVenue", team.PrimaryVenue);
                    cmd.Parameters.AddWithValue("@SVenue", team.SecondaryVenue);
                    cmd.Parameters.AddWithValue("@UserID", team.UserID);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (NotImplementedException ex)
            {
                throw ex;
            }
        }

        public void AdminUpdateTeam(Team team)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(AdminUpdateTeamSQL, conn);
                    cmd.Parameters.AddWithValue("@Name", team.Name);
                    cmd.Parameters.AddWithValue("@League", team.League);
                    cmd.Parameters.AddWithValue("@Org", team.Org);
                    cmd.Parameters.AddWithValue("@PVenue", team.PrimaryVenue);
                    cmd.Parameters.AddWithValue("@SVenue", team.SecondaryVenue);
                    cmd.Parameters.AddWithValue("@TeamID", team.TeamID);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (NotImplementedException ex)
            {
                throw ex;
            }
        }

        public void CreateLeague(League league)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(CreateLeagueSQL, conn);
                    cmd.Parameters.AddWithValue("@LeagueName", league.LeagueName);
                    cmd.Parameters.AddWithValue("@Org", league.Org);
                    cmd.Parameters.AddWithValue("@Sport", league.Sport);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<League> GetAllLeagues()
        {
            List<League> leagues = new List<League>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetAllLeaguesSQL, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        League league = new League
                        {
                            LeagueName = Convert.ToString(reader["leagueName"]),
                            Sport = Convert.ToString(reader["sport"]),
                            Org = Convert.ToString(reader["org"])
                        };

                        leagues.Add(league);
                    }
                }

                return leagues;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void UpdatePlayer(Player model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update roster set email = @email, firstName = @FirstName, lastName = @LastName, phone = @PhoneNumber where rosterID = @ID", conn);
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    cmd.Parameters.AddWithValue("@ID", model.ID);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (NotImplementedException ex)
            {
                throw ex;
            }
        }

        public void AddPlayer(Player player)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Insert into roster(email, firstName, lastName, phone, teamID) VALUES(@Email, @FirstName, @LastName, @PhoneNumber, @TeamID)", conn);
                    cmd.Parameters.AddWithValue("@Email", player.Email);
                    cmd.Parameters.AddWithValue("@FirstName", player.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", player.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", player.PhoneNumber);
                    cmd.Parameters.AddWithValue("@TeamID", player.TeamId);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void DeletePlayer(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete from roster where rosterID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private Team MapRowToTeam(SqlDataReader reader)
        {
            Team team = new Team
            {
                TeamID = Convert.ToInt32(reader["id"]),
                Name = Convert.ToString(reader["Name"]),
                League = Convert.ToString(reader["League"]),
                Org = Convert.ToString(reader["Org"]),
                PrimaryVenue = Convert.ToString(reader["PrimaryVenue"]),
                SecondaryVenue = Convert.ToString(reader["SecondaryVenue"])
            };

            if (!DBNull.Value.Equals(reader["UserID"]))
            {
                team.UserID = Convert.ToInt32(reader["UserID"]);
            }
            else
            {
                team.UserID = 0;
            }

            return team;
        }

        private Player MapRowToPlayer(SqlDataReader reader)
        {
            Player Player = new Player
            {
                ID = Convert.ToInt32(reader["rosterID"]),
                FirstName = Convert.ToString(reader["firstName"]),
                LastName = Convert.ToString(reader["lastName"]),
                TeamName = Convert.ToString(reader["Name"]),
            };
            if (!DBNull.Value.Equals(reader["email"]))
            {
                Player.Email = Convert.ToString(reader["email"]);
            }
            else
            {
                Player.Email = "";
            }
            if (!DBNull.Value.Equals(reader["phone"]))
            {
                Player.PhoneNumber = Convert.ToString(reader["Phone"]);
            }
            else
            {
                Player.PhoneNumber = "";
            }

            return Player;
        }
    }
}
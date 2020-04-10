using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class TeamSqlDAL
    {
        private readonly string connectionString;

        private string GetTeamsByLeagueSQL = "SELECT * from TEAMS WHERE League = @LeagueName Order by Name; ";
        private string GetLeagueByUserSQL = "SELECT League from TEAMS JOIN users on users.id = TEAMS.UserID WHERE users.id = @id; ";
        private string GetAllTeamsSQL = "SELECT * from TEAMS; ";
        private string GetTeamByUserIDSQL = "SELECT * from TEAMS WHERE UserID = @UserID; ";
        private string GetDatesByTeamIDSQL = "SELECT * from TEAMS JOIN EventDates on EventDates.TeamID = TEAMS.id  WHERE TeamID = @TeamID; ";
        private string InsertTeamSQL = "SELECT MAX(id) as max from users";
        private string InsertIntoTeamsSQL = "INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES (@Name, @League, @Org, @PrimaryVenue, @SecondaryVenue, @userID); ";
        private string UpdateTeamSQL = "UPDATE TEAMS SET Name = @Name, League = @League, Org = @Org, PrimaryVenue = @Pvenue, SecondaryVenue = @SVenue WHERE UserID = @UserID; ";

        public TeamSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
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

        public Team GetTeamByUserID(User user)
        {
            Team team = new Team();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetTeamByUserIDSQL, conn);
                    cmd.Parameters.AddWithValue("@UserID", user.Id);
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
                    SqlCommand cmd = new SqlCommand("SELECT * from TEAMS WHERE id = @TeamID;", conn);
                    cmd.Parameters.AddWithValue("@TeamID", TeamID);
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
                    SqlCommand cmd = new SqlCommand("UPDATE TEAMS SET Name = @Name, League = @League, Org = @Org, " +
                        "PrimaryVenue = @Pvenue, SecondaryVenue = @SVenue WHERE id = @TeamID; ", conn);
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO Leagues (leagueName, org, sport) " +
                        "VALUES (@LeagueName, @Org, @Sport);", conn);
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
                    SqlCommand cmd = new SqlCommand("SELECT * from Leagues", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        League league = new League();

                        league.LeagueName = Convert.ToString(reader["leagueName"]);
                        league.Sport = Convert.ToString(reader["sport"]);
                        league.Org = Convert.ToString(reader["org"]);

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

        private Team MapRowToTeam(SqlDataReader reader)
        {
            Team team = new Team();

            team.TeamID = Convert.ToInt32(reader["id"]);
            team.Name = Convert.ToString(reader["Name"]);
            team.League = Convert.ToString(reader["League"]);
            team.Org = Convert.ToString(reader["Org"]);
            team.PrimaryVenue = Convert.ToString(reader["PrimaryVenue"]);
            team.SecondaryVenue = Convert.ToString(reader["SecondaryVenue"]);
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
    }
}
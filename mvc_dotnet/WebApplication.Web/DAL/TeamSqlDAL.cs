using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class TeamSqlDAL
    {
        private readonly string connectionString;

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
                    SqlCommand cmd = new SqlCommand("SELECT * from TEAMS WHERE League = @LeagueName Order by Name; ", conn);
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
                    SqlCommand cmd = new SqlCommand("SELECT League from TEAMS JOIN users on users.id = TEAMS.UserID WHERE users.id = @id; ", conn);
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
                    SqlCommand cmd = new SqlCommand("SELECT * from TEAMS JOIN EventDates on EventDates.TeamID = TEAMS.id Order by Date; ", conn);

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
                    SqlCommand cmd = new SqlCommand("SELECT * from TEAMS WHERE UserID = @UserID; ", conn);
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



        public void InsertTeam(Team team)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MAX(id) as max from users", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    int id = 0;
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader["max"]);
                    }
                    reader.Close();


                    SqlCommand comd = new SqlCommand("INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue, UserID) VALUES (@Name, @League, @Org, @PrimaryVenue, @SecondaryVenue, @userID);", conn);
                    comd.Parameters.AddWithValue("@Name", team.Name);
                    comd.Parameters.AddWithValue("@League", team.League);
                    comd.Parameters.AddWithValue("@Org", team.Org);
                    comd.Parameters.AddWithValue("@PrimaryVenue", team.PrimaryVenue);
                    comd.Parameters.AddWithValue("@SecondaryVenue", team.SecondaryVenue);
                    comd.Parameters.AddWithValue("@userID", id);

                    comd.ExecuteNonQuery();

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
                    SqlCommand cmd = new SqlCommand("UPDATE TEAMS SET Name = @Name, League = @League, Org = @Org, " +
                        "PrimaryVenue = @Pvenue, SecondaryVenue = @SVenue WHERE UserID = @UserID; ", conn);
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







        private Team MapRowToTeam(SqlDataReader reader)
        {
            Team team = new Team();

            team.TeamID = Convert.ToInt32(reader["id"]);
            team.Name = Convert.ToString(reader["Name"]);
            team.League = Convert.ToString(reader["League"]);
            team.Org = Convert.ToString(reader["Org"]);
            team.PrimaryVenue = Convert.ToString(reader["PrimaryVenue"]);
            team.SecondaryVenue = Convert.ToString(reader["SecondaryVenue"]);
            team.UserID = Convert.ToInt32(reader["UserID"]);
            //if (Convert.ToInt32(reader["Home"]) == 1)
            //{
            //    team.HomeDates.Add(Convert.ToDateTime(reader["Date"]));
            //}
            //else
            //{
            //    team.TravelDates.Add(Convert.ToDateTime(reader["Date"]));
            //}

            return team;
        }
    }
}
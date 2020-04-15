using SportsClubOrganizer.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SportsClubOrganizer.Web.DAL
{
    public class UserSqlDAL : IUserDAL
    {
        private readonly string connectionString;

        private readonly string GetUserByIDSQL = "SELECT * FROM Users WHERE id = @id; ";
        private readonly string GetAllUnapprovedUserSQL = "SELECT * FROM UsersTemp;";
        private readonly string CreateUserSQL = "INSERT INTO Users(username, password, salt, role) VALUES (@username, @password, @salt, @role); ";
        private readonly string AdminApproveUserSQL = "INSERT INTO UsersTemp(username, password, salt, role) VALUES (@username, @password, @salt, @role); ";
        private readonly string DeleteUserSQL = "DELETE FROM Users WHERE id = @id; ";
        private readonly string DeleteUserTempSQL = "DELETE FROM UsersTemp WHERE id = @id; ";
        private readonly string GetUserSQL = "SELECT * FROM Users WHERE username = @username; ";
        private readonly string GetUserTempSQL = "SELECT * FROM UsersTemp WHERE username = @username; ";
        private readonly string UpdateUserSQL = "UPDATE Users SET password = @password, salt = @salt, role = @role WHERE id = @id; ";
        private readonly string GetUserLeagueNameSQL = "SELECT League FROM teams JOIN users on users.teamID = teams.id WHERE UserID = @userID; ";
        private readonly string GetUserFromTeamIDSQL = "SELECT userID from TEAMS where id = @teamID; ";

        public UserSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(CreateUserSQL, conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@salt", user.Salt);
                    cmd.Parameters.AddWithValue("@role", user.Role);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<User> GetAllUnapprovedUsers()
        {
            List<User> users = new List<User>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetAllUnapprovedUserSQL, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(MapRowToUser(reader));
                    }
                }

                return users;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void AdminApproveUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(AdminApproveUserSQL, conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@salt", user.Salt);
                    cmd.Parameters.AddWithValue("@role", user.Role);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void DeleteUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(DeleteUserSQL, conn);
                    cmd.Parameters.AddWithValue("@id", user.Id);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void DeleteUserTemp(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(DeleteUserTempSQL, conn);
                    cmd.Parameters.AddWithValue("@id", user.Id);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public User GetUser(string username)
        {
            User user = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetUserSQL, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }

                return user;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public User GetUserTemp(string username)
        {
            User user = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetUserTempSQL, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }

                return user;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public string GetUserLeagueName(User user)
        {
            string league = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetUserLeagueNameSQL, conn);
                    cmd.Parameters.AddWithValue("@userID", user.Id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
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

        public int GetUserFromTeamID(int? TeamID)
        {
            int userID = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetUserFromTeamIDSQL, conn);
                    cmd.Parameters.AddWithValue("@teamID", TeamID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userID = Convert.ToInt32(reader["userID"]);
                    }
                }

                return userID;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(UpdateUserSQL, conn);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@salt", user.Salt);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.Parameters.AddWithValue("@id", user.Id);

                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public User GetUserByID(int userID)
        {
            User user = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetUserByIDSQL, conn);
                    cmd.Parameters.AddWithValue("@id", userID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }

                return user;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private User MapRowToUser(SqlDataReader reader)
        {
            User user = new User();
            user.Id = Convert.ToInt32(reader["id"]);
            user.Username = Convert.ToString(reader["username"]);
            user.Password = Convert.ToString(reader["password"]);
            user.Salt = Convert.ToString(reader["salt"]);
            user.Role = Convert.ToString(reader["role"]);
            if (!DBNull.Value.Equals(reader["teamID"]))
            {
                user.TeamID = Convert.ToInt32(reader["teamId"]);
            }
            else
            {
                user.TeamID = 0;
            }
            return user;
        }
    }
}
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Models.Messages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SportsClubOrganizer.Web.DAL
{
    public class MessagesDAL
    {
        private readonly string connectionString;

        private string GetMessagesByUserSQL = "SELECT * from Messages WHERE toUserID = @SentTo; ";

        public MessagesDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<MessagesModel> GetMessagesByUser(User user)
        {
            List<MessagesModel> messages = new List<MessagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetMessagesByUserSQL, conn);
                    cmd.Parameters.AddWithValue("@SentTo", user.Id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MessagesModel message = new MessagesModel();
                        message.MessageBody = Convert.ToString(reader["messageBody"]);
                        message.SentByID = Convert.ToInt32(reader["SentByUserID"]);
                        message.SentToID = Convert.ToInt32(reader["toUserID"]);
                        message.ID = Convert.ToInt32(reader["id"]);
                        messages.Add(message);
                    }
                }
                return messages;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void AddMessageToDB(string message, int? userToID, int? userFromID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (@UserFrom, @UserTo, @messageBody)", conn);
                    cmd.Parameters.AddWithValue("@messageBody", message);
                    cmd.Parameters.AddWithValue("@UserTo", userToID);
                    cmd.Parameters.AddWithValue("@UserFrom", userFromID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public MessagesModel GetMessagebyID(int ID)
        {
            MessagesModel message = new MessagesModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from messages where id = @ID", conn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        message.MessageBody = Convert.ToString(reader["messageBody"]);
                        message.SentByID = Convert.ToInt32(reader["SentByUserID"]);
                        message.SentToID = Convert.ToInt32(reader["toUserID"]);
                    }
                }
                return message;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
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
                        message.UserAccepted = Convert.ToString(reader["userAccepted"]);
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

        public List<MessagesModel> GetMessagesForAdmin()
        {
            List<MessagesModel> messages = new List<MessagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Messages WHERE adminAccepted != 'Accepted' AND userAccepted = 'Accepted'", conn);
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

        public void AddMessageToDB(MessagesModel Message)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (@UserFrom, @UserTo, @messageBody)", conn);
                    cmd.Parameters.AddWithValue("@messageBody", Message.MessageBody);
                    cmd.Parameters.AddWithValue("@UserTo", Message.SentToID);
                    cmd.Parameters.AddWithValue("@UserFrom", Message.SentByID);
                    

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void UpdateMessage(MessagesModel Message)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Messages SET userAccepted = @UserAccepted WHERE id = @messageID", conn);
                    cmd.Parameters.AddWithValue("@messageID", Message.ID);
                    cmd.Parameters.AddWithValue("@UserAccepted", Message.UserAccepted);

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
            message.ID = ID;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from Messages where id = @ID", conn);
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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Messages;

namespace WebApplication.Web.DAL
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
    }
}
﻿using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Models.Messages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SportsClubOrganizer.Web.DAL
{
    public class MessagesDAL
    {
        private readonly string connectionString;

        private readonly string GetMessagesByUserSQL = "SELECT * from Messages WHERE toUserID = @SentTo; ";
        private readonly string GetMessagesResponsesSQL = "SELECT * from Messages WHERE SentByUserID = @Sentby AND userAccepted != 'no'; ";
        private readonly string GetMessagesForAdminSQL = "SELECT * FROM Messages WHERE adminAccepted != 'Accepted' AND userAccepted = 'Accepted'; ";
        private readonly string AddMessageToDBSQL = "INSERT INTO Messages(SentByUserID, toUserID, messageBody) VALUES (@UserFrom, @UserTo, @messageBody); ";
        private readonly string UpdateMessageAdminSQL = "UPDATE Messages SET adminAccepted = @AdminAccepted WHERE id = @messageID; ";
        private readonly string UpdateMessageUSQL = "UPDATE Messages SET userAccepted = @UserAccepted WHERE id = @messageID; ";
        private readonly string GetMessagebyIDSQL = "SELECT * from Messages where id = @ID; ";

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
                        message.AdminAccepted = Convert.ToString(reader["adminAccepted"]);
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

        public List<MessagesModel> GetMessagesResponses(User user)
        {
            List<MessagesModel> messages = new List<MessagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetMessagesResponsesSQL, conn);
                    cmd.Parameters.AddWithValue("@Sentby", user.Id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MessagesModel message = new MessagesModel();
                        message.MessageBody = Convert.ToString(reader["messageBody"]);
                        message.SentByID = Convert.ToInt32(reader["SentByUserID"]);
                        message.SentToID = Convert.ToInt32(reader["toUserID"]);
                        message.ID = Convert.ToInt32(reader["id"]);
                        message.UserAccepted = Convert.ToString(reader["userAccepted"]);
                        message.AdminAccepted = Convert.ToString(reader["adminAccepted"]);
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
                    SqlCommand cmd = new SqlCommand(GetMessagesForAdminSQL, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MessagesModel message = new MessagesModel();
                        message.MessageBody = Convert.ToString(reader["messageBody"]);
                        message.SentByID = Convert.ToInt32(reader["SentByUserID"]);
                        message.SentToID = Convert.ToInt32(reader["toUserID"]);
                        message.ID = Convert.ToInt32(reader["id"]);
                        message.AdminAccepted = Convert.ToString(reader["adminAccepted"]);
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

        public void AddGamePlayed(MessagesModel message)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into Games(teamID1, teamID2) VALUES(@teamID1, @teamID2)", conn);
                    cmd.Parameters.AddWithValue("@teamID1", message.SentByID);
                    cmd.Parameters.AddWithValue("@teamID2", message.SentToID);

                    cmd.ExecuteNonQuery();
                }
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
                    SqlCommand cmd = new SqlCommand(AddMessageToDBSQL, conn);
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
                    if ((Message.AdminAccepted == "Accepted") || (Message.AdminAccepted == "Declined"))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(UpdateMessageAdminSQL, conn);
                        cmd.Parameters.AddWithValue("@messageID", Message.ID);
                        cmd.Parameters.AddWithValue("@AdminAccepted", Message.AdminAccepted);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(UpdateMessageUSQL, conn);
                        cmd.Parameters.AddWithValue("@messageID", Message.ID);
                        cmd.Parameters.AddWithValue("@UserAccepted", Message.UserAccepted);
                        cmd.ExecuteNonQuery();
                    }
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
                    SqlCommand cmd = new SqlCommand(GetMessagebyIDSQL, conn);
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
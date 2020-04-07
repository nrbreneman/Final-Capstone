﻿using System;
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
                    SqlCommand cmd = new SqlCommand("SELECT * from TEAMS JOIN EventDates on EventDates.TeamID = TEAMS.id WHERE League = @LeagueName Order by Date; ", conn);
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

        public void InsertTeam(Team team)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO TEAMS (Name, League, Org, PrimaryVenue, SecondaryVenue) VALUES (@Name, @League, @Org, @PrimaryVenue, @SecondaryVenue);", conn);
                    cmd.Parameters.AddWithValue("@Name", team.Name);
                    cmd.Parameters.AddWithValue("@League", team.League);
                    cmd.Parameters.AddWithValue("@Org", team.Org);
                    cmd.Parameters.AddWithValue("@PrimaryVenue", team.PrimaryVenue);
                    cmd.Parameters.AddWithValue("@SecondaryVenue", team.SecondaryVenue);

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
            Team team = new Team();

            team.TeamID = Convert.ToInt32(reader["id"]);
            team.Name = Convert.ToString(reader["Name"]);
            team.League = Convert.ToString(reader["League"]);
            team.Org = Convert.ToString(reader["Org"]);
            team.PrimaryVenue = Convert.ToString(reader["PrimaryVenue"]);
            team.SecondaryVenue = Convert.ToString(reader["SecondaryVenue"]);
            if (Convert.ToInt32(reader["Home"]) == 1)
            {
                team.HomeDates.Add(Convert.ToDateTime(reader["Date"]));
            }
            else
            {
                team.TravelDates.Add(Convert.ToDateTime(reader["Date"]));
            }

            return team;
        }
    }
}
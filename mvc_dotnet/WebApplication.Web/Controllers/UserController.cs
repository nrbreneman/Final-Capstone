﻿using Microsoft.AspNetCore.Mvc;
using SportsClubOrganizer.Web.DAL;
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Models.Calendar;
using SportsClubOrganizer.Web.Providers.Auth;
using System.Collections.Generic;

namespace SportsClubOrganizer.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly TeamSqlDAL teamDAL;
        private readonly IUserDAL userDAL;

        public UserController(IAuthProvider authProvider, TeamSqlDAL teamDAL, IUserDAL userDAL)
        {
            this.authProvider = authProvider;
            this.teamDAL = teamDAL;
            this.userDAL = userDAL;
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult UserHomePage()
        {
            return View();
        }

        [HttpGet]
        [AuthorizationFilter("Admin", "User")]
        public IActionResult ViewMyLeague()
        {
            User user = authProvider.GetCurrentUser();
            string League = teamDAL.GetLeagueByUser(user);

            List<Team> teams = teamDAL.GetTeamsByLeague(League);
            return View(teams);
        }

        [HttpGet]
        [AuthorizationFilter("User", "Admin")]
        public IActionResult ChangeMyTeamInfo()
        {
            User user = authProvider.GetCurrentUser();
            user.UserTeam = teamDAL.GetTeamByUserID(user);
            user.UserTeam = teamDAL.GetDatesByTeamID(user);
            return View(user.UserTeam);
        }

        [HttpPost]
        [AuthorizationFilter("User", "Admin")]
        public IActionResult ChangeMyTeamInfo(Team team)
        {
            TempData["Added"] = "Successfully changed team info!";

            User user = authProvider.GetCurrentUser();
            team.UserID = user.Id;
            teamDAL.UpdateTeam(team);
            if (user.Role == "Admin")
            {
                return RedirectToAction("AdminHomePage", "Admin");
                //Admin login: madi.kohr@gmail.com, Password
            }
            else
            {
                return RedirectToAction("UserHomePage", "User");
            }
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult UpdateUserInfo()
        {
            User user = authProvider.GetCurrentUser();
            user = userDAL.GetUser(user.Username);
            return View(user);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult UpdateUserInfo(User user, string Salt, string NewPassword, string Password)
        {
            if (ModelState.IsValid)
            {
                user = authProvider.GetCurrentUser();
                authProvider.ChangePassword(Password, NewPassword);
                return RedirectToAction("UserHomePage", "User");
            }
            return View(user);
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public ActionResult AddAvailableDates()
        {
            Calendar calendar = new Calendar();
            User user = authProvider.GetCurrentUser();
            calendar.HomeDates = new List<System.DateTime?>();
            calendar.TravelDates = new List<System.DateTime?>();
            calendar.HomeDates = teamDAL.GetHomeDates(user);
            calendar.TravelDates = teamDAL.GetTravelDates(user);
            return View(calendar);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public ActionResult AddAvailableDates(Calendar calendar)
        {
            TempData["Added"] = "Successfully updated available dates";
            User user = authProvider.GetCurrentUser();
            teamDAL.AddHomeDateToDB(calendar.HomeDate, user);
            teamDAL.AddTravelDateToDB(calendar.TravelDate, user);

            //empModel.HomeDates = teamDAL.GetHomeDates(user);
            //empModel.TravelDates = teamDAL.GetTravelDates(user);
            return View(calendar);
        }

        [HttpGet]
        [AuthorizationFilter("Admin", "User")]
        public IActionResult ViewTeam(string League)
        {
            List<Team> teams = teamDAL.GetTeamsByLeague(League);
            return View(teams);
        }

        public IActionResult SeeSchedule()
        {
            User user = authProvider.GetCurrentUser();
            user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
            List<Game> games = teamDAL.GetScheduleByTeam(user.UserTeam);
            return View(games);
        }
    }
}
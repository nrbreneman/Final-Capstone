﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsClubOrganizer.Web.DAL;
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Models.Account;
using SportsClubOrganizer.Web.Models.Calendar;
using SportsClubOrganizer.Web.Providers.Auth;
using System.Collections.Generic;

namespace SportsClubOrganizer.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly TeamSqlDAL teamDAL;
        private readonly IUserDAL userDAL;

        public AdminController(IAuthProvider authProvider, TeamSqlDAL teamDAL, IUserDAL userDAL)
        {
            this.authProvider = authProvider;
            this.teamDAL = teamDAL;
            this.userDAL = userDAL;
        }

        [AuthorizationFilter("Admin")]
        public IActionResult AdminHomePage()
        {
            return View();
        }

        [AuthorizationFilter("Admin")]
        public IActionResult ViewAllTeams()
        {
            List<Team> teams = teamDAL.GetAllTeams();
            return View(teams);
        }

        private SelectListItem AddTeamToList(Team Team)

        {
            SelectListItem selectListItems = new SelectListItem();
            selectListItems = new SelectListItem { Text = Team.Name, Value = Team.TeamID.ToString() };
            return selectListItems;
        }

        private SelectListItem AddLeagueToList(string leagueName)
        {
            SelectListItem selectListItems = new SelectListItem();
            selectListItems = new SelectListItem { Text = leagueName, Value = leagueName };
            return selectListItems;
        }

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public IActionResult SelectLeague()
        {
            Team model = new Team();
            IList<League> leagues = teamDAL.GetAllLeagues();
            foreach (League league in leagues)
            {
                model.LeagueDropDown.Add(AddLeagueToList(league.LeagueName));
            }
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public IActionResult SelectLeague(Team team)
        {
            return RedirectToAction("ChangeATeamInfo", "Admin", new { team.League });
        }

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public IActionResult ChangeATeamInfo(string League)
        {
            Team model = new Team();
            List<Team> teamsByLeague = teamDAL.GetTeamsByLeague(League);

            foreach (Team team in teamsByLeague)
            {
                model.DropDownListTeam.Add(AddTeamToList(team));
            }

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public IActionResult ChangeATeamInfo(Team team)
        {
            team = teamDAL.GetTeamByTeamID(team.Name);
            return RedirectToAction("ChangeATeam", "Admin", team);
        }

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public IActionResult ChangeATeam(Team team)
        {
            //team = teamDAL.GetTeamByTeamID(team.TeamID.ToString());
            return View(team);
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public IActionResult ChangeATeam(Team team, int teamID)
        {
            TempData["Added"] = "Successfully updated " + team.Name + "'s team info";
            teamID = team.TeamID;
            teamDAL.AdminUpdateTeam(team);
            return RedirectToAction("AdminAddAvailableDates", "Admin", teamID);
        }

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public ActionResult AdminAddAvailableDates(int TeamID)
        {
            Calendar calendar = new Calendar();
            calendar.TeamID = TeamID;
            User user = new User();
            user.TeamID = calendar.TeamID;
            calendar.HomeDates = new List<System.DateTime?>();
            calendar.TravelDates = new List<System.DateTime?>();
            calendar.HomeDates = teamDAL.GetHomeDates(user);
            calendar.TravelDates = teamDAL.GetTravelDates(user);
            return View(calendar);
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public ActionResult AdminAddAvailableDates(Calendar calendar)
        {
            TempData["Added"] = "Successfully updated available dates";
            User user = new User();
            user.TeamID = calendar.TeamID;
            teamDAL.AddHomeDateToDB(calendar.HomeDate, user);
            teamDAL.AddTravelDateToDB(calendar.TravelDate, user);

            calendar.HomeDates = teamDAL.GetHomeDates(user);
            calendar.TravelDates = teamDAL.GetTravelDates(user);
            return View(calendar);
        }

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public IActionResult CreateNewLeague()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public IActionResult CreateNewLeague(League league)
        {
            TempData["Added"] = "Successfully created new league!";
            if (ModelState.IsValid)
            {
                teamDAL.CreateLeague(league);

                return RedirectToAction("AdminHomePage", "Admin");
            }

            return View(league);
        }

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public IActionResult CreateNewUser()
        {
            User user = new User();
            foreach (League league in teamDAL.GetAllLeagues())
            {
                user.LeagueDropDown.Add(AddLeagueToList(league.LeagueName));
            }
            return View(user);
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public IActionResult CreateNewUser(User user)
        {
            TempData["Added"] = "Successfully created new user!";
            RegisterViewModel model = new RegisterViewModel();
            model.Email = user.Username;
            model.ConfirmPassword = user.Password;
            model.Password = user.Password;

            if (ModelState.IsValid)
            {
                if (user.IsAdmin)
                {
                    authProvider.Register(model.Email, model.Password, role: "Admin");
                }
                else
                {
                    authProvider.Register(model.Email, model.Password, role: "User");
                }

                return RedirectToAction("AdminHomePage", "Admin");
            }

            return View(user);
        }

        [AuthorizationFilter("Admin")]
        public IActionResult FinalizeEvent()
        {
            return View();
        }

        [AuthorizationFilter("Admin")]
        public IActionResult ApproveUser()
        {
            return View();
        }

        public IActionResult SeeAvailability()
        {
            return View();
        }
    }
}
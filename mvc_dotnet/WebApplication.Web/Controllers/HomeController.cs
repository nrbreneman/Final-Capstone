﻿using Microsoft.AspNetCore.Mvc;
using SportsClubOrganizer.Web.DAL;
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Providers.Auth;
using System.Collections.Generic;
using System.Diagnostics;

namespace SportsClubOrganizer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly TeamSqlDAL teamDAL;
        public HomeController(TeamSqlDAL teamDAL)
        {
            this.teamDAL = teamDAL;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [AuthorizationFilter("Admin", "User")]
        public IActionResult ViewTeam(string League)
        {
            List<Team> teams = teamDAL.GetTeamsByLeague(League);
            return View(teams);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
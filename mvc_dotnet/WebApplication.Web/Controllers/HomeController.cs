using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;

namespace WebApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private TeamSqlDAL teamSqlDao;

        public HomeController(TeamSqlDAL teamSqlDao)
        {
            this.teamSqlDao = teamSqlDao;
        }

        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult AdminHomePage()
        {
            return View();
        }
        
        //public IActionResult UserHomePage()
        //{
        //    return View();
        //}

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ViewAllTeams()
        {
            List<Team> teams = teamSqlDao.GetAllTeams();
            return View(teams);
        }

        [HttpGet]
        public IActionResult ViewTeam(string League)
        {
            List<Team> teams = teamSqlDao.GetTeamsByLeague(League);
            return View(teams);
        }

        [HttpGet]
        public IActionResult UserHomePage()
        {
            return View();
        }


        //private User GetUserInfo()
        //{
        //    User user = null;

        //    if (HttpContext.Session.Get<User>("User") == null)
        //    {
        //        user = new User();
        //        SaveUser(user);
        //    }
        //    else
        //    {
        //        user = HttpContext.Session.Get<User>("User");
        //    }

        //    return user;
        //}

        //private void SaveUser(User user)
        //{
        //    HttpContext.Session.Set("User", user);
        //}







        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

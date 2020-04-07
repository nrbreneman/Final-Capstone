using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly TeamSqlDAL teamDAL;
        public HomeController(IAuthProvider authProvider, TeamSqlDAL teamDAL)
        {
            this.authProvider = authProvider;
            this.teamDAL = teamDAL;
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
            List<Team> teams = teamDAL.GetAllTeams();
            return View(teams);
        }

        [HttpGet]
        public IActionResult ViewTeam(string League)
        {
            List<Team> teams = teamDAL.GetTeamsByLeague(League);
            return View(teams);
        }

        [HttpGet]
        public IActionResult UserHomePage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewMyLeague()
        {
            User user = authProvider.GetCurrentUser();
            string League = teamDAL.GetLeagueByUser(user);
            
            List<Team> teams = teamDAL.GetTeamsByLeague(League);
            return View(teams);
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

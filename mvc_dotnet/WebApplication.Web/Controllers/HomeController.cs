using Microsoft.AspNetCore.Mvc;
using SportsClubOrganizer.Web.DAL;
using SportsClubOrganizer.Web.Models;
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

        public IActionResult Whoop()
        {
            return View();
        }


        public IActionResult Faygo()
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
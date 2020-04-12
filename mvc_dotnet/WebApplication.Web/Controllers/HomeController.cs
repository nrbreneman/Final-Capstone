using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsClubOrganizer.Web.DAL;
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Providers.Auth;
using System.Collections.Generic;
using System.Diagnostics;

namespace SportsClubOrganizer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly TeamSqlDAL teamDAL;
        private readonly IUserDAL userDAL;

        public HomeController(IAuthProvider authProvider, TeamSqlDAL teamDAL, IUserDAL userDAL)
        {
            this.authProvider = authProvider;
            this.teamDAL = teamDAL;
            this.userDAL = userDAL;
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



        //Moved to AdminController
        //[HttpGet]--------Moved to UserController
        //[AuthorizationFilter("Admin", "User")]
        //public IActionResult ViewMyLeague()
        //{
        //    User user = authProvider.GetCurrentUser();
        //    string League = teamDAL.GetLeagueByUser(user);
        //    List<Team> teams = teamDAL.GetTeamsByLeague(League);
        //    return View(teams);
        //}
        //private Team AddTeamNames(Team model)
        //{
        //    IList<Team> teamNames = teamDAL.GetAllTeams();
        //    foreach (Team s in teamNames)
        //    {
        //        model.AddGenre(s);
        //    }
        //    return model;
        //}
        //private SelectListItem AddTeamToList(string teamName)
        //{
        //    SelectListItem selectListItems = new SelectListItem();
        //    selectListItems = new SelectListItem { Text = teamName, Value = teamName };
        //    return selectListItems;
        //}
        //private SelectListItem AddTeamToList(Team Team)
        //{
        //    SelectListItem selectListItems = new SelectListItem();
        //    selectListItems = new SelectListItem { Text = Team.Name, Value = Team.TeamID.ToString() };
        //    return selectListItems;
        //}
        //private SelectListItem AddLeagueToList(string leagueName)
        //{
        //    SelectListItem selectListItems = new SelectListItem();
        //    selectListItems = new SelectListItem { Text = leagueName, Value = leagueName };
        //    return selectListItems;
        //}
        //[HttpGet]
        //[AuthorizationFilter("Admin")]
        //public IActionResult ChangeATeamInfo()
        //{
        //    Team model = new Team();
        //    IList<Team> teams = teamDAL.GetAllTeams();
        //    foreach (Team team in teams)
        //    {
        //        model.DropDownListTeam.Add(AddTeamToList(team.Name));
        //    }


            

        //Moved To UserController
        //public IActionResult SeeSchedule()
        //{
        //    User user = authProvider.GetCurrentUser();
        //    user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
        //    List<Game> games = teamDAL.GetScheduleByTeam(user.UserTeam);
        //    return View(games);
        //}


            

        //NOT USED
        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";
        //    return View();
        //}
        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";
        //    return View();
        //}
        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        //NOT USED
        //public ActionResult Calendar()
        //{
        //    //https://localhost:44392/home/calendar
        //    //https://localhost:44392/home/calendar2
        //    return View();
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using WebApplication.Web.DAL;
using WebApplication.Web.DAL.Models;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Account;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Controllers
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

        [AuthorizationFilter("Admin")]
        public IActionResult AdminHomePage()
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

        [AuthorizationFilter("Admin")]
        public IActionResult ViewAllTeams()
        {
            List<Team> teams = teamDAL.GetAllTeams();
            return View(teams);
        }

        [HttpGet]
        [AuthorizationFilter("Admin", "User")]
        public IActionResult ViewTeam(string League)
        {
            List<Team> teams = teamDAL.GetTeamsByLeague(League);
            return View(teams);
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
<<<<<<< HEAD
            TempData["Added"] = "Successfully changed team info!";

=======
            TempData["Added"] = "Successfully updated team info!";
>>>>>>> 02355b9b49c13a8c66573e568b418f29cd860507
            User user = authProvider.GetCurrentUser();
            team.UserID = user.Id;
            teamDAL.UpdateTeam(team);
            return RedirectToAction("UserHomePage", "Home");
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
                return RedirectToAction("UserHomePage", "Home");
            }
            return View(user);
        }

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

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public IActionResult SelectLeague()
        {
            Team model = new Team();
            IList<Team> teams = teamDAL.GetAllTeams();
            HashSet<string> teamHash = new HashSet<string>();
            foreach (Team team in teams)
            {
                teamHash.Add(team.League);
            }

            foreach (string league in teamHash)
            {
                model.LeagueDropDown.Add(AddLeagueToList(league));
            }
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public IActionResult SelectLeague(Team team)
        {
            return RedirectToAction("ChangeATeamInfo", "Home", new { team.League });
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
            team = teamDAL.GetTeamByTeamID(team.TeamID.ToString());
            return RedirectToAction("ChangeATeam", "Home", team);
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
            return RedirectToAction("AdminHomePage", "Home");
        }

        public ActionResult Calendar()
        {
            //https://localhost:44392/home/calendar
            //https://localhost:44392/home/calendar2
            return View();
        }

        [HttpGet]
        public ActionResult AddAvailableDates()
        {
            EmpModel empModel = new EmpModel();
            empModel.HomeDates = new List<System.DateTime?>();
            empModel.TravelDates = new List<System.DateTime?>();
            return View(empModel);
        }

        [HttpPost]
        public ActionResult AddAvailableDates(EmpModel empModel)
        {
            User user = authProvider.GetCurrentUser();
            teamDAL.AddHomeDateToDB(empModel.HomeDate, user);
            teamDAL.AddTravelDateToDB(empModel.TravelDate, user);

            empModel.HomeDates = teamDAL.GetHomeDates(user);
            empModel.TravelDates = teamDAL.GetTravelDates(user);
            return View(empModel);
        }

        //public ActionResult ClickTravelButton(Team team, DateTime TravelDate)
        //{
        //    team.TravelDates.Add(TravelDate);
        //    return View(TravelDate);
        //}

        //public ActionResult ClickHomeButton(Team team, DateTime HomeDate)
        //{
        //    team.HomeDates.Add(HomeDate);
        //    return c
        //}

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

                return RedirectToAction("AdminHomePage", "Home");
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

                //userDAL.CreateUser(user);

                return RedirectToAction("AdminHomePage", "Home");
            }

            return View(user);
        }

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
using Microsoft.AspNetCore.Mvc;
using SportsClubOrganizer.Web.DAL;
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Models.Calendar;
using SportsClubOrganizer.Web.Providers.Auth;
using System.Collections.Generic;
using System.Linq;

namespace SportsClubOrganizer.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly TeamSqlDAL teamDAL;
        private readonly IUserDAL userDAL;
        private readonly MessagesDAL messageDAL;

        public UserController(IAuthProvider authProvider, TeamSqlDAL teamDAL, IUserDAL userDAL, MessagesDAL messageDAL)
        {
            this.authProvider = authProvider;
            this.teamDAL = teamDAL;
            this.userDAL = userDAL;
            this.messageDAL = messageDAL;
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult UserHomePage()
        {
            return View();
        }

        [HttpGet]
        [AuthorizationFilter("Admin", "User")]
        public IActionResult ViewMyLeague(string SortOrder)
        {
            User user = authProvider.GetCurrentUser();
            string League = teamDAL.GetLeagueByUser(user);
            List<Team> teams = new List<Team>();
            List<Team> teams1 = teamDAL.GetTeamsByLeague(League);
            foreach(Team team in teams1)
            {
                if (team.TeamID != user.TeamID)
                {
                    teams.Add(team);
                }
            }
            foreach (Team team in teams)
            {
                team.count = teamDAL.GetCountOfTimesPlayed(user, team.TeamID);
                
                team.HomeDates = teamDAL.GetHomeDates(team.TeamID.ToString());
                team.TravelDates = teamDAL.GetTravelDates(team.TeamID.ToString());
            }

            if (SortOrder == "HomeAvailable")
            {
                teams = teams.OrderBy(t => t.HomeDates.OrderBy(d => d.Value).First()).ToList();
            }
            else if (SortOrder == "TravelAvailable")
            {
                teams = teams.OrderBy(t => t.TravelDates.OrderBy(d => d.Value).First()).ToList();
            }
            else if (SortOrder == "TimesPlayed")
            {
                teams = teams.OrderBy(t => t.count).ToList();
                
            }
            return View(teams);
        }

        [HttpGet]
        [AuthorizationFilter("User", "Admin")]
        public IActionResult ChangeMyTeamInfo()
        {
            User user = authProvider.GetCurrentUser();
            user.UserTeam = teamDAL.GetTeamByUserID(user.Id);
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
            calendar.HomeDates = teamDAL.GetHomeDates(user.TeamID.ToString());
            calendar.TravelDates = teamDAL.GetTravelDates(user.TeamID.ToString());
            return View(calendar);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public ActionResult AddAvailableDates(Calendar calendar)
        {
            TempData["Added"] = "Successfully updated available dates";
            User user = authProvider.GetCurrentUser();
            //user.TeamID = teamDAL.GetTeamByUserID(user.Id);
            if(calendar.HomeDate != null)
            {
                teamDAL.AddHomeDateToDB(calendar.HomeDate, user);
            }
            if (calendar.TravelDate != null)
            {
                teamDAL.AddTravelDateToDB(calendar.TravelDate, user);
            }
            calendar.HomeDates = teamDAL.GetHomeDates(user.TeamID.ToString());
            calendar.TravelDates = teamDAL.GetTravelDates(user.TeamID.ToString());

            return View(calendar);
        }

        [HttpGet]
        [AuthorizationFilter("Admin", "User")]
        public IActionResult ViewTeam(string League)
        {
            List<Team> teams = teamDAL.GetTeamsByLeague(League);
            return View(teams);
        }

        [AuthorizationFilter("User")]
        public IActionResult SeeSchedule()
        {
            User user = authProvider.GetCurrentUser();
            user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
            List<Game> games = teamDAL.GetScheduleByTeam(user.UserTeam);
            return View(games);
        }

        [AuthorizationFilter("User")]
        public IActionResult ViewMyRoster()
        {
            User user = authProvider.GetCurrentUser();
            List<Player> roster = teamDAL.GetRoster(user.TeamID.Value);
            return View(roster);
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult AddAPlayer()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult AddAPlayer(Player player)
        {
            User user = authProvider.GetCurrentUser();
            player.TeamId = user.TeamID.Value;
            teamDAL.AddPlayer(player);
            return RedirectToAction("ViewMyRoster", "User");
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult UpdateAPlayer(int ID)
        {
            Player player = teamDAL.GetPlayerByID(ID);
            return View(player);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult UpdateAPlayer(Player model)
        {
            teamDAL.UpdatePlayer(model);
            return RedirectToAction("ViewMyRoster", "User");
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult DeletePlayer()
        {
            User user = authProvider.GetCurrentUser();
            List<Player> roster = teamDAL.GetRoster(user.TeamID.Value);
            return View(roster);
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult DeleteAPlayer(int id)
        {
            teamDAL.DeletePlayer(id);
            return RedirectToAction("ViewMyRoster", "User");
        }
    }
}
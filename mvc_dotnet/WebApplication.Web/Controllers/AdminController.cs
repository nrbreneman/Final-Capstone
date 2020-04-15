using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsClubOrganizer.Web.DAL;
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Models.Account;
using SportsClubOrganizer.Web.Models.Calendar;
using SportsClubOrganizer.Web.Models.Messages;
using SportsClubOrganizer.Web.Providers.Auth;
using System.Collections.Generic;

namespace SportsClubOrganizer.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly TeamSqlDAL teamDAL;
        private readonly IUserDAL userDAL;
        private readonly MessagesDAL messageDAL;

        public AdminController(IAuthProvider authProvider, TeamSqlDAL teamDAL, IUserDAL userDAL, MessagesDAL messageDAL)
        {
            this.authProvider = authProvider;
            this.teamDAL = teamDAL;
            this.userDAL = userDAL;
            this.messageDAL = messageDAL;
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
            foreach (Team team in teams)
            {
                team.HomeDates = teamDAL.GetHomeDates(team.TeamID.ToString());
                team.TravelDates = teamDAL.GetTravelDates(team.TeamID.ToString());
            }
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
            calendar.HomeDates = teamDAL.GetHomeDates(user.TeamID.ToString());
            calendar.TravelDates = teamDAL.GetTravelDates(user.TeamID.ToString());
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

            calendar.HomeDates = teamDAL.GetHomeDates(user.TeamID.ToString());
            calendar.TravelDates = teamDAL.GetTravelDates(user.TeamID.ToString());
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
            List<MessagesModel> messages = messageDAL.GetMessagesForAdmin();
            Team team = new Team();
            foreach (MessagesModel message in messages)
            {
                team = teamDAL.GetTeamByUserID(message.SentByID);
                message.SentByName = team.Name;
                team = teamDAL.GetTeamByUserID(message.SentToID);
                message.SentToName = team.Name;
                messageDAL.AddGamePlayed(message);
            }
            return View(messages);
        }

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public IActionResult ApproveUser()
        {
            List<User> users = new List<User>();
            users = userDAL.GetAllUnapprovedUsers();
            return View(users);
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public IActionResult AcceptUser(string username)
        {
            User user = new User();
            user = userDAL.GetUserTemp(username);
            userDAL.CreateUser(user);
            userDAL.DeleteUserTemp(user);
            return RedirectToAction("ApproveUser", "Admin");
        }

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public IActionResult DeclineUser(string username)
        {
            User user = new User();
            user = userDAL.GetUserTemp(username);
            userDAL.DeleteUserTemp(user);
            return RedirectToAction("ApproveUser", "Admin");
        }

        public IActionResult SeeAvailability()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public IActionResult AdminAcceptEvent(int id)
        {
            TempData["Final"] = "You have approved this event both teams will be notified";
            MessagesModel Message = messageDAL.GetMessagebyID(id);
            Message.AdminAccepted = "Accepted";
            messageDAL.UpdateMessage(Message);
            return RedirectToAction("FinalizeEvent", "Admin");
        }

        [HttpPost]
        [AuthorizationFilter("Admin")]
        public IActionResult AdminDeclineEvent(int id)
        {
            TempData["Final"] = "You have declined this event both teams will be notified";
            MessagesModel Message = messageDAL.GetMessagebyID(id);
            Message.AdminAccepted = "Declined";
            messageDAL.UpdateMessage(Message);
            return RedirectToAction("FinalizeEvent", "Admin");
        }

        [HttpGet]
        [AuthorizationFilter("Admin")]
        public IActionResult AdminSeeSchedule(int id)
        {
            Team team = new Team();
            team = teamDAL.GetTeamByTeamID(id.ToString());
            List<Game> games = teamDAL.GetScheduleByTeam(team);
            return View(games);
        }
    }
}
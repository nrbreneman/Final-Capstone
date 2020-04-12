using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsClubOrganizer.Web.DAL;
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Models.Messages;
using SportsClubOrganizer.Web.Providers.Auth;
using System.Collections.Generic;

namespace SportsClubOrganizer.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly TeamSqlDAL teamDAL;
        private readonly IUserDAL userDAL;
        private readonly MessagesDAL messageDAL;

        public MessageController(IAuthProvider authProvider, TeamSqlDAL teamDAL, IUserDAL userDAL, MessagesDAL messageDAL)
        {
            this.authProvider = authProvider;
            this.teamDAL = teamDAL;
            this.userDAL = userDAL;
            this.messageDAL = messageDAL;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMessage()
        {
            return View();
        }

        public IActionResult SeeMessages()
        {
            User user = authProvider.GetCurrentUser();
            List<MessagesModel> messages = messageDAL.GetMessagesByUser(user);
            return View(messages);
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult SelectTeamToSendMessageTo()
        {
            User user = authProvider.GetCurrentUser();
            user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
            string league = userDAL.GetUserLeagueName(user);
            List<Team> teamsByLeague = teamDAL.GetTeamsByLeague(league);

            foreach (Team team in teamsByLeague)
            {
                user.UserTeam.DropDownListTeam.Add(AddTeamToList(team));
            }
            Team team1 = new Team();
            return View(team1);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult SelectTeamToSendMessageTo(Team team)
        {
            return RedirectToAction("SelectHomeOrAwayVenue", "Message", team);
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult SelectHomeOrAwayVenue(Team team)
        {
            User user = authProvider.GetCurrentUser();
            return View(user);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult SelectHomeOrAwayVenue(User user)
        {
            return RedirectToAction("SelectDate", "Message", user);
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult SelectDate()
        {
            User user = authProvider.GetCurrentUser();
            user.UserTeam.HomeDates = teamDAL.GetHomeDates(user);
            user.UserTeam.TravelDates = teamDAL.GetTravelDates(user);
            return View(user);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult SelectDate(User user)
        {
            return RedirectToAction("SendMessages", "Message", user);
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult SendMessages(User user)
        {
            return View(user);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult SendMessages(User userTo, string message)
        {
            User userFrom = authProvider.GetCurrentUser();
            int userToTeamID = userDAL.GetUserFromTeamID(userTo.TeamID);
            messageDAL.AddMessageToDB(message, userTo.TeamID, userFrom.TeamID);
            return RedirectToAction("UserHomePage", "User");
        }

        public IActionResult SeeAvailability()
        {
            return View();
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
    }
}
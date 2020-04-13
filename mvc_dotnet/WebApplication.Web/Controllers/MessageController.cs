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

            return View(user.UserTeam);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult SelectTeamToSendMessageTo(Team team)
        {
            return RedirectToAction("CreateMessage", "Message", new { team.Name });
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult CreateMessage(string Name)
        {
            Team OpposingTeam = teamDAL.GetTeamByTeamID(Name);
            OpposingTeam.HomeDates = teamDAL.GetHomeDates(Name);
            OpposingTeam.TravelDates = teamDAL.GetTravelDates(Name);
            return View(OpposingTeam);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult CreateMessage(Team OpposingTeam)
        {
            return View();
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult SelectHomeOrAwayVenue(string Name)
        {
            User user = authProvider.GetCurrentUser();
            user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
            Team OpponentTeam = teamDAL.GetTeamByTeamID(Name);
            Venue SelectedVenue = new Venue();
            SelectedVenue.Team = user.UserTeam;
            SelectedVenue.PrimaryVenueName = user.UserTeam.PrimaryVenue;
            SelectedVenue.SecondaryVenueName = user.UserTeam.SecondaryVenue;
            return View(SelectedVenue);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult SelectHomeOrAwayVenue(Venue SelectedVenue)
        {
            User user = authProvider.GetCurrentUser();
            user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
            user.UserTeam.SelectedVenue = SelectedVenue.SelectedVenueName;
            return RedirectToAction("SelectDate", "Message", user);
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult SelectDate(User user)
        {
            //user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
            user.UserTeam.HomeDates = teamDAL.GetHomeDates(user.TeamID.ToString());
            user.UserTeam.TravelDates = teamDAL.GetTravelDates(user.TeamID.ToString());
            return View(user);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult SelectDate()
        {
            User user = authProvider.GetCurrentUser();
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
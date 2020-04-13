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

        

        [AuthorizationFilter("User")]
        public IActionResult SeeMessages()
        {
            User user = authProvider.GetCurrentUser();
            List<MessagesModel> messages = messageDAL.GetMessagesByUser(user);
            Team team = new Team();
            foreach (MessagesModel message in messages)
            {
                team = teamDAL.GetTeamByUserID(message.SentByID);
                message.SentByName = team.Name;
            }
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
            return RedirectToAction("CreateMessage", "Message",  new { team.Name });
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
        public IActionResult CreateMessage(int UserID, string Message)
        {
            TempData["Added"] = "Your Message Has Been Sent!";
            User user = authProvider.GetCurrentUser();
            MessagesModel message = new MessagesModel();
            message.MessageBody = Message;
            message.SentToID = UserID;
            message.SentByID = user.TeamID;
            messageDAL.AddMessageToDB(message);
            return RedirectToAction("UserHomePage", "User");
        }
        
        [AuthorizationFilter("User")]
        public IActionResult UserAcceptEvent(int id)
        {
            TempData["Final"] = "You have approved this event, now pending Admin approval";
            MessagesModel Message = messageDAL.GetMessagebyID(id);
            Message.UserAccepted = "Accepted";
            messageDAL.UpdateMessage(Message);
            return RedirectToAction("SeeMessages", "Message");
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult UserDeclineEvent(int ID)
        {
            TempData["Final"] = "You have declined this event, the other team will be notified";
            MessagesModel message = messageDAL.GetMessagebyID(ID);
            message.UserAccepted = "Declined";
            messageDAL.UpdateMessage(message);
            return RedirectToAction("SeeMessages", "Message");
        }

        //[HttpGet]
        //[AuthorizationFilter("User")]
        //public IActionResult SelectHomeOrAwayVenue(string Name)
        //{
        //    User user = authProvider.GetCurrentUser();
        //    user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
        //    Team OpponentTeam = teamDAL.GetTeamByTeamID(Name);
        //    Venue SelectedVenue = new Venue();
        //    SelectedVenue.Team = user.UserTeam;
        //    SelectedVenue.PrimaryVenueName = user.UserTeam.PrimaryVenue;
        //    SelectedVenue.SecondaryVenueName = user.UserTeam.SecondaryVenue;
        //    return View(SelectedVenue);
        //}

        //[HttpPost]
        //[AuthorizationFilter("User")]
        //public IActionResult SelectHomeOrAwayVenue(Venue SelectedVenue)
        //{
        //    User user = authProvider.GetCurrentUser();
        //    user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
        //    user.UserTeam.SelectedVenue = SelectedVenue.SelectedVenueName;
        //    return RedirectToAction("SelectDate", "Message", user);
        //}

        //[HttpGet]
        //[AuthorizationFilter("User")]
        //public IActionResult SelectDate(User user)
        //{
        //    //user.UserTeam = teamDAL.GetTeamByTeamID(user.TeamID.ToString());
        //    user.UserTeam.HomeDates = teamDAL.GetHomeDates(user.TeamID.ToString());
        //    user.UserTeam.TravelDates = teamDAL.GetTravelDates(user.TeamID.ToString());
        //    return View(user);
        //}

        //[HttpPost]
        //[AuthorizationFilter("User")]
        //public IActionResult SelectDate()
        //{
        //    User user = authProvider.GetCurrentUser();
        //    return RedirectToAction("SendMessages", "Message", user);
        //}

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
            MessagesModel Message = new MessagesModel();
            Message.MessageBody = message;
            Message.SentToID = userTo.TeamID;
            Message.SentByID = userFrom.TeamID;
            messageDAL.AddMessageToDB(Message);
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Messages;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Controllers
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

        private SelectListItem AddTeamToList(Team Team)

        {
            SelectListItem selectListItems = new SelectListItem();
            selectListItems = new SelectListItem { Text = Team.Name, Value = Team.TeamID.ToString() };
            return selectListItems;
        }

        [HttpGet]
        [AuthorizationFilter("User")]
        public IActionResult SendMessages()
        {
            Team model = new Team();
            User user = authProvider.GetCurrentUser();
            model.League = user.League.LeagueName;
            List<Team> teamsByLeague = teamDAL.GetTeamsByLeague(model.League);

            foreach (Team team in teamsByLeague)
            {
                model.DropDownListTeam.Add(AddTeamToList(team));
            }

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter("User")]
        public IActionResult SendMessages(Team team)
        {
            team = teamDAL.GetTeamByTeamID(team.Name);
            return RedirectToAction("ChangeATeam", "Home", team);
        }
    }
}
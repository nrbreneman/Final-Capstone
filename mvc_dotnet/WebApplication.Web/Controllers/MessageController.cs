using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly TeamSqlDAL teamDAL;
        private readonly IUserDAL userDAL;

        public MessageController(IAuthProvider authProvider, TeamSqlDAL teamDAL, IUserDAL userDAL)
        {
            this.authProvider = authProvider;
            this.teamDAL = teamDAL;
            this.userDAL = userDAL;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
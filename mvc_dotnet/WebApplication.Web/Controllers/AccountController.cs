using Microsoft.AspNetCore.Mvc;
using SportsClubOrganizer.Web.DAL;
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Models.Account;
using SportsClubOrganizer.Web.Providers.Auth;

namespace SportsClubOrganizer.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly TeamSqlDAL teamDAL;

        public AccountController(IAuthProvider authProvider, TeamSqlDAL teamDAL)
        {
            this.authProvider = authProvider;
            this.teamDAL = teamDAL;
        }

        [AuthorizationFilter("Admin", "User")]
        [HttpGet]
        public IActionResult Index()
        {
            var user = authProvider.GetCurrentUser();
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                bool validLogin = authProvider.SignIn(loginViewModel.Email, loginViewModel.Password);
                User user = authProvider.GetCurrentUser();
                if (validLogin)
                {
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("AdminHomePage", "Admin");
                        //Admin login: admin@gmail.com, Password
                    }
                    else
                    {
                        return RedirectToAction("UserHomePage", "User");
                    }
                }
            }

            ModelState.AddModelError("password", "Username and/or Password incorrect. Please try again");
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult LogOff()
        {
            TempData["LogOut"] = "You have successfully logged out!";
            authProvider.LogOff();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                authProvider.Register(registerViewModel.Email, registerViewModel.Password, role: "User");

                return RedirectToAction(nameof(RegisterTeam));
            }

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult RegisterTeam()
        {
            Team team = new Team();
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterTeam(Team team)
        {
            if (ModelState.IsValid)
            {
                teamDAL.InsertTeam(team);
                TempData["Registered"] = "You have submitted your registration, please wait for an Admin to approve your account";
                return RedirectToAction("Index", "Home");
            }

            return View(team);
        }
    }
}
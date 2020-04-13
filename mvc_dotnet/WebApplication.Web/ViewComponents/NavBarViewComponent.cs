﻿using Microsoft.AspNetCore.Mvc;
using SportsClubOrganizer.Web.Models;
using SportsClubOrganizer.Web.Providers.Auth;

namespace SportsClubOrganizer.Web.ViewComponents
{
    /// <summary>
    /// A view component is a reusable or "isolated" piece of our app.
    /// It cannot be navigated to via URL like a controller.
    /// </summary>
    public class NavBarViewComponent : ViewComponent
    {
        // Components allow dependency injection just like controllers.
        private IAuthProvider authProvider;

        public NavBarViewComponent(IAuthProvider authProvider)
        {
            this.authProvider = authProvider;
        }

        /// <summary>
        /// This is the method that is invoked when the component is told to "render".
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke(User user)
        {
            user = authProvider.GetCurrentUser();
            return View("_NavBar", user);
        }
    }
}
using SportsClubOrganizer.Web.Models;
using System.Collections.Generic;

namespace SportsClubOrganizer.Web.DAL
{
    public interface IUserDAL
    {
        User GetUser(string username);

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);

        string GetUserLeagueName(User user);

        int GetUserFromTeamID(int? TeamID);

        List<User> GetAllUnapprovedUsers();

        User GetUserTemp(string username);

        void DeleteUserTemp(User user);
    }
}
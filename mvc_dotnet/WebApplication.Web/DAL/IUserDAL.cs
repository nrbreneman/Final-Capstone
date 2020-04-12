using SportsClubOrganizer.Web.Models;

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
    }
}
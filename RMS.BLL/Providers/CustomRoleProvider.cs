using System;
using System.Web.Security;
using RMS.BLL.IManager;
using RMS.BLL.Manager;

namespace RMS.BLL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly IUserManager _userManager;
        public CustomRoleProvider()
        {
            _userManager = new UserManager();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool isUserInRole = _userManager.IsUserInRole(username, roleName);
            return isUserInRole;
        }

        public override string[] GetRolesForUser(string username)
        {
            return _userManager.UserRolesByUserName(username);
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

    }
}
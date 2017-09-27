using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;

namespace PL.Providers
{
	public class CustomRoleProvider : RoleProvider
	{
		public IUserService UserService { get; private set; }
		public IRoleService RoleService { get; private set; }

		public CustomRoleProvider()
		{
			RoleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
			UserService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
		}
		
		public override bool IsUserInRole(string login, string roleName)
        {
            var user = UserService.GetUserByLogin(login);

            if (user == null)
                return false;

            return RoleService.GetUserRoles(user.Id).Any(role => role.Name == roleName);
        }

        public override string[] GetRolesForUser(string login)
        {
            var user = UserService.GetUserByLogin(login);

            if (user == null)
                return null;           

            return RoleService.GetUserRoles(user.Id) != null 
				? RoleService.GetUserRoles(user.Id).Select(r => r.Name).ToArray() 
				: null;
        }

        public override void CreateRole(string roleName)
        {
            var newRole = new RoleEntity() { Name = roleName };
            RoleService.CreateRole(newRole);
        }

        #region Stubs

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
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

        #endregion
	}
}
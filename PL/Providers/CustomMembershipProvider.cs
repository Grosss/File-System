using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using RoleEntity = BLL.Interface.Entities.RoleEntity;

namespace PL.Providers
{
	public class CustomMembershipProvider : MembershipProvider
	{
		public IUserService UserService { get; private set; }
		public IRoleService RoleService { get; private set; }

		public CustomMembershipProvider()
		{
			RoleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
			UserService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
		}

		public MembershipUser CreateUser(string email, string login, string password)
        {
            MembershipUser membershipUser = GetUser(login, false);

            if (membershipUser != null)
                return null;

			UserEntity user = new UserEntity
            {
                Email = email,
                Login = login,
                Password = Crypto.HashPassword(password)
            };
            
            UserService.CreateUser(user);
	        RoleEntity userRole = RoleService.GetAllRoles().SingleOrDefault(r => r.Name == "user");
	        if (userRole != null)
			{ 
		        RoleService.AddRoleToUser(UserService.GetUserByLogin(user.Login).Id, 
			        userRole.Id);
			}
	        membershipUser = GetUser(login, false);
            return membershipUser;
        }

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            UserEntity user = UserService.GetUserByLogin(login);

            if (user == null)
                return null;

			MembershipUser memberUser = new MembershipUser("CustomMembershipProvider", user.Login,
                null, user.Email, null, null,
                false, false, DateTime.Now,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public override bool ValidateUser(string login, string password)
        {
	        UserEntity user = UserService.GetUserByLogin(login);

	        return user != null && Crypto.VerifyHashedPassword(user.Password, password);
        }

        #region Stubs

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipUser CreateUser(string login, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }        

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }        

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        #endregion 
	}
}
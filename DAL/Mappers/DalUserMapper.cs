using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
	public static class DalUserMapper
	{
		public static DalUser ToDalUser(this User user)
		{
			var dalUser = new DalUser
			{
				Id = user.UserId,
				Email = user.Email,
				Login = user.Login,
				Password = user.Password
			};

			return dalUser;
		}

		public static User ToOrmUser(this DalUser user)
		{
			var ormUser = new User
			{
				UserId = user.Id,
				Email = user.Email,
				Login = user.Login,
				Password = user.Password
			};

			return ormUser;
		}
	}
}
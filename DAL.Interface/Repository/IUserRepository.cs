using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
	public interface IUserRepository : IRepository<DalUser>
	{
		DalUser GetUserByEmail(string email);
		DalUser GetUserByLogin(string login);
	}
}
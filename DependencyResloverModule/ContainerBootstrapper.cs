using BLL.Interface.Services;
using BLL.Services;
using DAL;
using DAL.Concrete;
using DAL.Interface;
using DAL.Interface.Repository;
using Microsoft.Practices.Unity;

namespace DependencyResloverModule
{
	public class ContainerBootstrapper
    {
		public static void RegisterTypes(IUnityContainer container)
		{
			container.RegisterType<IUnitOfWork, UnitOfWork>();
			container.RegisterType<IUnitOfWork, UnitOfWork>();

			container.RegisterType<IUserRepository, UserRepository>();
			container.RegisterType<IRoleRepository, RoleRepository>();

			container.RegisterType<IUserService, UserService>();
			container.RegisterType<IRoleService, RoleService>();
		}
    }
}

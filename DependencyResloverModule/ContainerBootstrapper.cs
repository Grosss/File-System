using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using DAL;
using DAL.Concrete;
using DAL.Interface;
using DAL.Interface.Repository;
using Microsoft.Practices.Unity;
using ORM;

namespace DependencyResloverModule
{
	public class ContainerBootstrapper
    {
		public static void RegisterTypes(IUnityContainer container)
		{
			container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
			container.RegisterType<DbContext, FileSystemContext>(new HierarchicalLifetimeManager());

			container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IRoleRepository, RoleRepository>(new HierarchicalLifetimeManager());

			container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
			container.RegisterType<IRoleService, RoleService>(new HierarchicalLifetimeManager());
		}
    }
}

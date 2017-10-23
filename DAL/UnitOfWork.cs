using System.Data.Entity;
using DAL.Interface;

namespace DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		public DbContext Context { get; private set; }

		public UnitOfWork(DbContext context)
		{
			Context = context;
		}

		public void Commit()
		{
			if (Context != null)
			{
				Context.SaveChanges();
			}
		}
	}
}
using System;

namespace DAL.Interface
{
	public interface IUnitOfWork
	{
		void Commit();
	}
}
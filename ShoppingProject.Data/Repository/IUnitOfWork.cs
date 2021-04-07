using System;
using System.Threading.Tasks;

namespace ShoppingProject.Data.Interface
{
    public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync();
	}
}
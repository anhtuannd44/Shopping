using ShoppingProject.Data.Interface;
using System.Threading.Tasks;

namespace ShoppingProject.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbFactory _dbFactory;

        public UnitOfWork(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// Save changes to database.
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await _dbFactory.DbContext.SaveChangesAsync();
        }
    }
}

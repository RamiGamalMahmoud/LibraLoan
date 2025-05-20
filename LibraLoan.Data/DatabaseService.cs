using LibraLoan.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Data
{
    internal class DatabaseService : IDatabaseService
    {
        public DatabaseService(IAppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> CanConnectAsync()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Database.CanConnectAsync();
            }
        }

        public async Task MigrateAsync()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (await HasPendingMigrationsAsync())
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
        }

        public async Task<bool> HasPendingMigrationsAsync()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return (await dbContext.Database.GetPendingMigrationsAsync()).Any();
            }
        }

        private readonly IAppDbContextFactory _dbContextFactory;

    }
}

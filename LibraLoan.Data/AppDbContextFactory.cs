using LibraLoan.Core.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace LibraLoan.Data
{
    internal class AppDbContextFactory(IConnectionStringFactory connectionStringFactory) : IAppDbContextFactory
    {
        public AppDbContext CreateDbContext()
        {
            return new AppDbContext(new DbContextOptionsBuilder()
                .UseSqlite(_connectionStringFactory.GetConnectionString())
                .Options);
        }

        private readonly IConnectionStringFactory _connectionStringFactory = connectionStringFactory;
    }
}

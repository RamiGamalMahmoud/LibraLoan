using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraLoan.Data
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine(args.Length.ToString());
            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder
                .UseSqlite("Data Source = .\\DB\\LibraLoan.db;Password=fEr4uLNHktXtHcx;");

            AppDbContext dbContext = new AppDbContext(builder.Options);
            return dbContext;
        }
    }
}

using LibraLoan.Core.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace LibraLoan.Data
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();
            services.AddSingleton<IAppDbContextFactory, AppDbContextFactory>();
            services.AddSingleton<Core.Abstraction.Repositories.IUsersRepository, Repositories.UsersRepository>();

            services.AddSingleton<Core.Abstraction.Repositories.IPermissionsRepository, Repositories.PermissionsRepository>();
            services.AddSingleton<Core.Abstraction.Repositories.IRolesRepository, Repositories.RolesRepository>();
            services.AddSingleton<Core.Abstraction.Repositories.IAuthorsRepository, Repositories.AuthorsRepository>();
            services.AddSingleton<Core.Abstraction.Repositories.IPublishersRepository, Repositories.PublishersRepository>();
            services.AddSingleton<Core.Abstraction.Repositories.IBooksRepository, Repositories.BooksRepository>();
            services.AddSingleton<Core.Abstraction.Repositories.IClientsRepository, Repositories.ClientsReository>();
            services.AddSingleton<Core.Abstraction.Repositories.ILoansRepository, Repositories.LoansRepository>();
            return services;
        }
    }
}

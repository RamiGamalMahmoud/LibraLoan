using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction;
using LibraLoan.Data;
using LibraLoan.Features.Auth;
using Microsoft.Extensions.DependencyInjection;
using LibraLoan.Features.Management;
using LibraLoan.Core;
using LibraLoan.Services;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Features.Authors;
using LibraLoan.Features.Publishers;
using LibraLoan.Features.Books;
using LibraLoan.Features.Clients;
using LibraLoan.Features.Loans;

namespace LibraLoan
{
    internal static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
            services.AddSingleton<IDirectoriesService, Services.DirectoriesService>();
            services.AddSingleton<IAppStateService, AppStateService>();
            services.AddSingleton<Core.Abstraction.INotificationsService, Services.NotificationsService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();

            services.ConfigureDataServices();
            services.ConfigureAuthFeature();
            services.ConfigureCoreServices();
            services.ConfigureManagementFeature();
            services.ConfigureAuthersFeature();
            services.ConfigurePublishersFeature();
            services.ConfigureBooksFeature();
            services.ConfigureClientsFeature();
            services.ConfigureLoandFeature();

            return services;
        }
    }
}

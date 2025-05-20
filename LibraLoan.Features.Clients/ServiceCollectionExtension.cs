using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraLoan.Features.Clients
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureClientsFeature(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddSingleton<Core.Abstraction.Features.Clients.IClientsListingView, Listing.View>();
            services.AddSingleton<Listing.ViewModel>();
            services.AddSingleton<Editor.ViewModelCreate>();
            return services;
        }
    }
}

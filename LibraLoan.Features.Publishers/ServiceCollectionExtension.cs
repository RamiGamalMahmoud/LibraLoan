using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraLoan.Features.Publishers
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigurePublishersFeature(this IServiceCollection services)
        {
            services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<Core.Abstraction.Features.Publishers.IPublishersListingView, Listing.View>();
            services.AddSingleton<Listing.ViewModel>();
            services.AddTransient<Editor.ViewModelCreate>();
            return services;
        }
    }
}

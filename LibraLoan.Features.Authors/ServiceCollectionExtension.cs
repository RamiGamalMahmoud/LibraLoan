using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraLoan.Features.Authors
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureAuthersFeature(this IServiceCollection services)
        {
            services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<Core.Abstraction.Features.Authors.IAuthorsListingView, Listing.View>();
            services.AddSingleton<Listing.ViewModel>();
            services.AddTransient<Editor.ViewModelCreate>();
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraLoan.Features.Books
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureBooksFeature(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddSingleton<Core.Abstraction.Features.Books.IBooksListingView, Listing.View>();
            services.AddSingleton<Listing.ViewModel>();
            services.AddTransient<Editor.ViewModelCreate>();
            return services;
        }
    }
}

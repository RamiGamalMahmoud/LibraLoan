using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraLoan.Features.Loans
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureLoandFeature(this IServiceCollection services)
        {
            services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<Core.Abstraction.Features.Loans.ILoansListingView, Listing.View>();
            services.AddSingleton<Listing.ViewModel>();
            services.AddTransient<Editor.ViewModelCreate>();
            return services;
        }
    }
}

using LibraLoan.Core.Abstraction.Features.Auth.Login;
using Microsoft.Extensions.DependencyInjection;

namespace LibraLoan.Features.Auth
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureAuthFeature(this IServiceCollection services)
        {
            services.AddTransient<ILoginView, Login.View>();
            services.AddTransient<Login.ViewModel>();
            return services;
        }
    }
}

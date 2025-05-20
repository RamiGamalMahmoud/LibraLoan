using LibraLoan.Core.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace LibraLoan.Core
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, Common.PasswordHasher>();
            return services;
        }
    }
}

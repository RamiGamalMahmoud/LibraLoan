using LibraLoan.Core.Abstraction.Features.Management;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraLoan.Features.Management
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureManagementFeature(this IServiceCollection services)
        {
            services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<Home.ViewModel>();
            services.AddSingleton<IManagementHomeView, Home.View>();
            services.AddSingleton<Users.Listing.View>();
            services.AddSingleton<Users.Listing.ViewModel>();

            services.AddTransient<Users.Editor.ViewModelCreate>();
            services.AddTransient<Users.Editor.ViewModelUpdate>();

            services.AddSingleton<RolesAndPermissions.View>();
            services.AddSingleton<RolesAndPermissions.ViewModel>();

            services.AddTransient<RolesAndPermissions.Permissions.Editor.ViewModelCreate>();
            services.AddSingleton<RolesAndPermissions.Permissions.ViewModel>();

            services.AddSingleton<RolesAndPermissions.Roles.ViewModel>();
            services.AddTransient<RolesAndPermissions.Roles.Editor.ViewModelCreate>();
            services.AddTransient<RolesAndPermissions.Roles.Editor.View>();
            return services;
        }
    }
}

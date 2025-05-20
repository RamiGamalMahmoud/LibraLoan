using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Models;
using LibraLoan.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace LibraLoan.Tests
{
    public class ServicesConfiguration
    {
        public ServicesConfiguration()
        {

            Provider = BuildServiceProvider();
        }

        private IServiceProvider BuildServiceProvider()
        {
            ConfigureServices();
            ConfiguraPublishersFeature();
            ConfiguraUsersFeature();
            ConfigureBooksFeature();
            ConfigureLoansFeature();

            return services.BuildServiceProvider();
        }

        private void ConfigureServices()
        {
            services.AddSingleton<IConnectionStringFactory, TestConnectionStringFactory>();
            services.AddSingleton<IPasswordHasher, LibraLoan.Core.Common.PasswordHasher>();
            services.AddSingleton<IAppDbContextFactory, TestAppDbContextFactory>();
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
            services.AddSingleton<IAppStateService>(s =>
            {
                User user = new User() { Id = 1, Username = "user", IsActive = true };
                Moq.Mock<IAppStateService> mock = new Moq.Mock<IAppStateService>();
                mock.Setup(x => x.CurrentUser).Returns(user);
                return mock.Object;
            });
        }

        private void ConfiguraUsersFeature()
        {
            services.AddSingleton<LibraLoan.Features.Management.Users.Editor.ViewModelUpdate>();
            services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(s =>
            {
                IMediator mediator = s.GetRequiredService<IMediator>();
                IMessenger messenger = s.GetRequiredService<IMessenger>();
                IPasswordHasher passwordHasher = s.GetRequiredService<IPasswordHasher>();

                User admin = new User() { Username = "user", IsActive = true };
                Role role = new Role();
                User user = new User() { Username = "user", IsActive = true, CreatedBy = admin, Role = role };

                return new LibraLoan.Features.Management.Users.Editor.ViewModelUpdate(mediator, messenger, passwordHasher, user);
            });
        }

        private void ConfiguraPublishersFeature()
        {
            services.AddTransient(s =>
            {
                IMediator mediator = s.GetRequiredService<IMediator>();
                IMessenger messenger = s.GetRequiredService<IMessenger>();

                Publisher publisher = new Publisher() { Name = "publisher", Phone = "123456789", Email = "email" };

                LibraLoan.Features.Publishers.Editor.ViewModelUpdate viewModel = new LibraLoan.Features.Publishers.Editor.ViewModelUpdate(mediator, messenger, publisher);

                return viewModel;
            });
        }

        private void ConfigureBooksFeature()
        {
            services.AddTransient< LibraLoan.Features.Books.Editor.ViewModelCreate>();

            services.AddTransient(s =>
            {
                IMediator mediator = s.GetRequiredService<IMediator>();
                IMessenger messenger = s.GetRequiredService<IMessenger>();
                Book book = new Book() { Title = "book", Isbn = "123456789", PublishDate = DateTime.Now, Version = 1, Copies = 1, Publisher = new Publisher() };
                book.Authors.Add(new Author() { Name = "author" });
                return new LibraLoan.Features.Books.Editor.ViewModelUpdate(mediator, messenger, book);
            });
        }

        private void ConfigureLoansFeature()
        {
            services.AddTransient<LibraLoan.Features.Loans.Editor.ViewModelCreate>();
        }

        public IServiceProvider Provider { get; }

        private readonly IServiceCollection services = new ServiceCollection();

    }
}

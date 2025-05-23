using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction;
using LibraLoan.Core.Abstraction.Features.Auth.Login;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace LibraLoan
{
    public partial class App : Application, IAppStates
    {
        public App()
        {
            _host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IAppStates>(this);
                    services.AddServices();
                })
                .Build();

            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            _messenger = _host.Services.GetRequiredService<IMessenger>();
            _notificationsService = _host.Services.GetRequiredService<Core.Abstraction.INotificationsService>();
            _appState = _host.Services.GetRequiredService<IAppStateService>();

            RegisterMessages();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
#if DEBUG
            MessageBox.Show(e.Exception.Message);
#endif
#if !DEBUG
            e.Handled = true;
#endif
        }

        private void RegisterMessages()
        {
            _messenger.Register<Core.Messages.LoginFailedMessage>(this, (r, m) =>
            {
                _notificationsService.ShowErrorMessage("خطأ في تسجيل الدخول");
            });

            _messenger.Register<Core.Messages.UserLoggedInMessage>(this, (r, m) =>
            {
                _notificationsService.ShowSuccessMessage("تم تسجيل الدخول بنجاح");
                _appState.SetCurrentUser(m.User);
                ShowMainWindow();
            });

            _messenger.Register<Core.Messages.Common.LogoutMessage>(this, (r, m) => Restart());

            _messenger.Register<Core.Messages.Common.ConfigrRequestMessge>(this, (r, m) =>
            {
                ConfirmMessageView confirmMessageView = new(m.Message);
                bool? result = confirmMessageView.ShowDialog();
                m.Reply(result is true);
            });
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Velopack.VelopackApp.Build().Run();

            MainWindow = new Window();
            IDirectoriesService directoriesService = _host.Services.GetRequiredService<IDirectoriesService>();
            directoriesService.CreateDirectories();
            directoriesService.CreateDatabase();

            IDatabaseService databaseService = _host.Services.GetRequiredService<IDatabaseService>();

            if (!await databaseService.CanConnectAsync())
            {
                _notificationsService.ShowErrorMessage("لا يمكن الاتصال بقاعدة البيانات");
                await Task.Delay(3000);
                Current.Shutdown();
            }

            _notificationsService.ShowSuccessMessage("تم الاتصال بقاعدة البيانات");

            if (await databaseService.HasPendingMigrationsAsync())
            {
                await databaseService.MigrateAsync();
                _notificationsService.ShowSuccessMessage("تم تحديث قاعدة البيانات");
            }

            ShowLogin();
        }

        private void ShowLogin()
        {
            ILoginView loginView = _host.Services.GetRequiredService<ILoginView>();

            MainWindow = loginView as Window;
            MainWindow.Show();
        }

        private void ShowMainWindow()
        {
            Window window = MainWindow;
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            _notificationsService.ShowTrayIcon();
            window.Close();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _notificationsService.Dispose();
        }

        private void Logout()
        {
            Window window = MainWindow;
            MainWindow = _host.Services.GetRequiredService<ILoginView>() as Window;
            MainWindow.Show();
            window.Close();
        }

        public void Restart()
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Shutdown();
        }

        public void SetState<T>(State<T> state)
        {
            throw new NotImplementedException();
        }

        public State<T> GetState<T>()
        {
            throw new NotImplementedException();
        }

        public void DeleteState<T>()
        {
            throw new NotImplementedException();
        }

        private readonly IHost _host;
        private readonly IMessenger _messenger;
        private readonly INotificationsService _notificationsService;
        private readonly IAppStateService _appState;
    }

}

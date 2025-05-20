using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core;
using LibraLoan.Core.Abstraction.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace LibraLoan
{
    internal partial class MainViewModel : ObservableObject
    {
        public MainViewModel(IServiceProvider serviceProvider, IAppStateService appStateService)
        {
            _serviceProvider = serviceProvider;
            AppStateService = appStateService;
            IntializeCommands();

            WeakReferenceMessenger.Default.Register<Core.Messages.Common.ShowDialogMessage>(this, (r, m) =>
            {
                object dialog = _serviceProvider.GetRequiredService(m.Type);
                DialogContent = dialog;
            });

        }

        public ObservableCollection<NavigationCommand> NavigationCommands { get; } = new ObservableCollection<NavigationCommand>();

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsDialogOpen))]
        private object _dialogContent;

        public bool IsDialogOpen => DialogContent is not null;

        public IAppStateService AppStateService { get; }

        private void IntializeCommands()
        {
            NavigationCommands.Add(new NavigationCommand("المتسخدمون و الصلاحيات", "AccountGroup", () => SetCurrentView(typeof(Core.Abstraction.Features.Management.IManagementHomeView))));
            NavigationCommands.Add(new NavigationCommand("المؤلفون", "AccountTie", () => SetCurrentView(typeof(Core.Abstraction.Features.Authors.IAuthorsListingView))));
            NavigationCommands.Add(new NavigationCommand("دور النشر", "HospitalBuilding", () => SetCurrentView(typeof(Core.Abstraction.Features.Publishers.IPublishersListingView))));
            NavigationCommands.Add(new NavigationCommand("الكتب", "Bookshelf", () => SetCurrentView(typeof(Core.Abstraction.Features.Books.IBooksListingView))));
            NavigationCommands.Add(new NavigationCommand("العملاء", "AccountGroupOutline", () => SetCurrentView(typeof(Core.Abstraction.Features.Clients.IClientsListingView))));
            NavigationCommands.Add(new NavigationCommand("الاستعارة", "SwapHorizontal", () => SetCurrentView(typeof(Core.Abstraction.Features.Loans.ILoansListingView))));
        }

        [ObservableProperty]
        private NavigationCommand _command;

        [RelayCommand]
        private void Logout()
        {
            WeakReferenceMessenger.Default.Send(new Core.Messages.Common.LogoutMessage());
        }

        partial void OnCommandChanged(NavigationCommand oldValue, NavigationCommand newValue)
        {
            if (newValue is null || newValue.Action is null)
            {
                CurrentView = null;
                return;
            }
            newValue.Action.Invoke();
        }

        private void SetCurrentView(Type type)
        {
            try
            {
                CurrentView = type is null ? null : _serviceProvider.GetRequiredService(type);
            }
            catch (InvalidOperationException)
            {
                WeakReferenceMessenger.Default.Send(new Core.Messages.Common.ErrorMessage(type.Name));
                WeakReferenceMessenger.Default.Send(new Core.Messages.Common.ErrorMessage("الصفحة غير موجودة"));
            }
        }

        private readonly IServiceProvider _serviceProvider;
    }
}

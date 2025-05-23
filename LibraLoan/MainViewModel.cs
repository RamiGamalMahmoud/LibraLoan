using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using ActionModel = LibraLoan.Core.Models.Action;
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
            NavigationCommands.Add(new NavigationCommand("المتسخدمون و الصلاحيات",
                "AccountGroup",
                () => SetCurrentView(typeof(Core.Abstraction.Features.Management.IManagementHomeView)),
                AppStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.ReadAction)));


            NavigationCommands.Add(new NavigationCommand("المؤلفون",
                "AccountTie",
                () => SetCurrentView(typeof(Core.Abstraction.Features.Authors.IAuthorsListingView)),
                AppStateService.CurrentUser.HasPermission(Resource.AuthorsResource, ActionModel.ReadAction)));

            NavigationCommands.Add(new NavigationCommand("دور النشر",
                "HospitalBuilding",
                () => SetCurrentView(typeof(Core.Abstraction.Features.Publishers.IPublishersListingView)),
                AppStateService.CurrentUser.HasPermission(Resource.PublishersResource, ActionModel.ReadAction)));

            NavigationCommands.Add(new NavigationCommand("الكتب",
                "Bookshelf",
                () => SetCurrentView(typeof(Core.Abstraction.Features.Books.IBooksListingView)),
                AppStateService.CurrentUser.HasPermission(Resource.BooksResource, ActionModel.ReadAction)));

            NavigationCommands.Add(new NavigationCommand("العملاء",
                "AccountGroupOutline",
                () => SetCurrentView(typeof(Core.Abstraction.Features.Clients.IClientsListingView)),
                AppStateService.CurrentUser.HasPermission(Resource.ClientsResource, ActionModel.ReadAction)));

            NavigationCommands.Add(new NavigationCommand("الاستعارة",
                "SwapHorizontal",
                () => SetCurrentView(typeof(Core.Abstraction.Features.Loans.ILoansListingView)),
                AppStateService.CurrentUser.HasPermission(Resource.LoansResource, ActionModel.ReadAction)));
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

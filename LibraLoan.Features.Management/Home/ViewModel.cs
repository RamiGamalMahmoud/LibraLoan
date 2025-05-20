using CommunityToolkit.Mvvm.ComponentModel;
using LibraLoan.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace LibraLoan.Features.Management.Home
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IServiceProvider serviceProvider)
        {
            NavigationCommands.Add(new NavigationCommand("المستخدمون", null, () => SetCurrentView(typeof(Management.Users.Listing.View))));
            NavigationCommands.Add(new NavigationCommand("الصلاحيات", null, () => SetCurrentView(typeof(Management.RolesAndPermissions.View))));
            _serviceProvider = serviceProvider;
        }
        public ObservableCollection<NavigationCommand> NavigationCommands { get; } = new ObservableCollection<NavigationCommand>();

        [ObservableProperty]
        private NavigationCommand _navigationCommand;

        partial void OnNavigationCommandChanged(NavigationCommand oldValue, NavigationCommand newValue)
        {
            if(newValue is null || newValue.Action is null)
            {
                CurrentView = null;
                return;
            }

            newValue.Action.Invoke();
        }

        private void SetCurrentView(Type type)
        {
            CurrentView = type is null ? null : _serviceProvider.GetRequiredService(type);
        }

        [ObservableProperty]
        private object _currentView;

        private readonly IServiceProvider _serviceProvider;
    }
}

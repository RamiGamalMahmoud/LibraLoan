using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Models;

namespace LibraLoan.Services
{
    internal partial class AppStateService : ObservableObject, IAppStateService
    {
        public AppStateService()
        {
            WeakReferenceMessenger.Default.Register<Core.Messages.UserLoggedInMessage>(this, (r, m) => SetCurrentUser(m.User));
            WeakReferenceMessenger.Default.Register<Core.Messages.GetLoggedInUser>(this, (r, m) => m.Reply(CurrentUser));
        }

        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            private set => SetProperty(ref _currentUser, value);
        }

        public string AppVersion => "0.0.15";

        public void SetCurrentUser(User user) => CurrentUser = user;
    }
}

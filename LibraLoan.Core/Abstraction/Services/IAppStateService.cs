using LibraLoan.Core.Models;

namespace LibraLoan.Core.Abstraction.Services
{
    public interface IAppStateService
    {
        public string AppVersion { get; }
        User CurrentUser { get; }
        public void SetCurrentUser(User user);
    }
}

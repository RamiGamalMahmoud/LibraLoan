using System;

namespace LibraLoan.Core.Abstraction
{
    public interface INotificationsService : IDisposable
    {
        void ShowErrorMessage(string message);
        void ShowSuccessMessage(string message);
        void ShowTrayIcon();
    }
}

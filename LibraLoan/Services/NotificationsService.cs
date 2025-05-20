using CommunityToolkit.Mvvm.Messaging;
using Notification.Wpf;
using System.Windows.Forms;

namespace LibraLoan.Services
{
    internal class NotificationsService : Core.Abstraction.INotificationsService
    {
        public NotificationsService(IMessenger messenger)
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = new System.Drawing.Icon("LibraLoan.ico");

            messenger.Register<Core.Messages.Common.SuccessMessage>(this, (r, m) => ShowSuccessMessage(m.Message));
            messenger.Register<Core.Messages.Common.ErrorMessage>(this, (r, m) => ShowErrorMessage(m.Message));
        }

        public void ShowTrayIcon()
        {
            _notifyIcon.Visible = true;
            _notificationManager.Show("", "مرحبا", NotificationType.Information);
        }

        public void ShowInfoMessage(string message)
        {
            _notificationManager.Show("", message, NotificationType.Information);
        }

        public void ShowErrorMessage(string message)
        {
            _notificationManager.Show("", message, NotificationType.Error);
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
        }

        public void ShowSuccessMessage(string message)
        {
            _notificationManager.Show("", message, NotificationType.Success);
        }

        private readonly NotifyIcon _notifyIcon;
        private readonly NotificationManager _notificationManager = new NotificationManager();
        private readonly IMessenger _messenger;
    }
}

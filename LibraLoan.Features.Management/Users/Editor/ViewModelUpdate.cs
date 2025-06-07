using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.Users.Editor
{
    internal partial class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, IPasswordHasher passwordHasher, User user) : base(mediator, messenger, passwordHasher)
        {
            _user = user;
            UserName = user.Username;
            SelectedRole = user.Role;
            IsActive = user.IsActive;
            UserCanBeDeactivated = !user.IsAdmin;
            _notifyPropertiesNames.Add(nameof(Password));
            HasChanges = false;
        }

        public override string Title => "تعديل مستخدم";
        private User _user;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _password;

        protected override async Task Save()
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge("هل تريد حفظ التعديلات ؟"));
            if (!isConfirmed) return;
            string hashedPassword = string.Empty;
            if (!string.IsNullOrEmpty(Password))
            {
                hashedPassword = _passwordHasher.HashPassword(Password);
            }

            bool isUpdated = await _mediator.Send(new Core.Commands.Common.UpdateCommand<UserDto>(new UserDto(_user.Id, UserName.Trim(), hashedPassword, SelectedRole, null, IsActive)));
            if (isUpdated)
            {
                _user.Username = UserName;
                _user.Role = SelectedRole;
                _user.IsActive = IsActive;
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم التحديث بنجاح"));
                return;
            }

            _messenger.Send(new Core.Messages.Common.ErrorMessage("اسم مستخدم موجود مسبقا"));
        }
    }
}

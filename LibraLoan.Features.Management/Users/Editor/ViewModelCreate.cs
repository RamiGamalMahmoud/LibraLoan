using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.Users.Editor
{
    internal partial class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger, IPasswordHasher passwordHasher) : base(mediator, messenger, passwordHasher)
        {
            IsActive = true;
            _notifyPropertiesNames.Add(nameof(Password));
        }

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال كلمة المرور")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyPropertyChangedFor(nameof(HasChanges))]
        private string _password;

        public override string Title => "إضافة مستخدم";

        protected override async Task Save()
        {
            User currentUser = _messenger.Send(new Core.Messages.GetLoggedInUser());
            User user = await _mediator.Send(new Core.Commands.Common.CreateCommand<User, UserDto>(new UserDto(0, UserName, _passwordHasher.HashPassword(Password), SelectedRole, currentUser, true)));

            if (user is not null)
            {
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الاضافة بنجاح"));
                return;
            }

            _messenger.Send(new Core.Messages.Common.ErrorMessage("فشل الاضافة"));
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.Users.Editor
{
    internal abstract partial class ViewModel : EditorViewModelBase
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IPasswordHasher passwordHasher)
        {
            _mediator = mediator;
            _messenger = messenger;
            _passwordHasher = passwordHasher;
            _notifyPropertiesNames = [nameof(UserName), nameof(IsActive), nameof(SelectedRole)];
        }

        public async Task LoadDataAsync()
        {
            Roles = await _mediator.Send(new Core.Commands.Common.GetAllCommand<Role>(false));
        }

        [ObservableProperty]
        private bool _userCanBeDeactivated = true;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال اسم المستخدم")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _userName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private bool _isActive;

        [ObservableProperty]
        private IEnumerable<Role> _roles;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب تحديد دور المستخدم")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private Role _selectedRole;

        [RelayCommand]
        protected virtual void ClearInputs()
        {
            UserName = null;
            SelectedRole = null;
            IsActive = true;
            HasChanges = false;
        }

        protected IMediator _mediator;
        protected IMessenger _messenger;

        protected readonly IPasswordHasher _passwordHasher;
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
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
            ValidateAllProperties();
        }

        public async Task LoadDataAsync()
        {
            Roles = await _mediator.Send(new Core.Commands.Common.GetAllCommand<Role>(false));
        }

        private bool _userCanBeDeactivated = true;
        public bool UserCanBeDeactivated
        {
            get => _userCanBeDeactivated;
            protected set => SetProperty(ref _userCanBeDeactivated, value);
        }

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



        protected IMediator _mediator;
        protected IMessenger _messenger;

        protected readonly IPasswordHasher _passwordHasher;
    }
}

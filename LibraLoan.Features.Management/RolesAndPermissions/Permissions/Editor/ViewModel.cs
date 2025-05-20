using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.Permissions.Editor
{
    internal abstract partial class ViewModel : EditorViewModelBase
    {
        public ViewModel(IMediator mediator, IMessenger messenger)
        {
            _mediator = mediator;
            _messenger = messenger;
            _notifyPropertiesNames = [nameof(SelectedResource), nameof(SelectedAction)];
            ValidateAllProperties();
        }

        public async Task LoadAsync()
        {
            Actions = await _mediator.Send(new Core.Commands.Common.GetAllCommand<Action>(true));
            Resources = await _mediator.Send(new Core.Commands.Common.GetAllCommand<Resource>(true));
        }

        [ObservableProperty]
        private bool _isDone;

        [ObservableProperty]
        [Required(ErrorMessage = "يرجى اختيار نوع الصلاحية")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private Resource _selectedResource;

        [ObservableProperty]
        [Required(ErrorMessage = "يرجى اختيار نوع الإجراء")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private Action _selectedAction;

        [ObservableProperty]
        private IEnumerable<Action> _actions;

        [ObservableProperty]
        private IEnumerable<Resource> _resources;

        protected readonly IMediator _mediator;
        protected readonly IMessenger _messenger;
    }
}

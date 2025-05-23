using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.Roles.Editor
{
    internal abstract partial class ViewModel : EditorViewModelBase
    {
        public ViewModel(IMediator mediator, IMessenger messenger)
        {
            _mediator = mediator;
            _messenger = messenger;

            HasChanges = false;
            ValidateAllProperties();
        }

        [RelayCommand]
        protected async virtual Task LoadAsync(bool reload)
        {
            Permissions = (await _mediator.Send(new Core.Commands.Common.GetAllCommand<Permission>(reload)))
                .Select(x => new SelectableObject<Permission>(x))
                .ToList();

            Resources = Permissions.GroupBy(x => x.Value.Resource.Name);
            PermissionsGroups = Resources.Select(x => new PermissionsGroup(x.Key, x.ToList())).ToList();
        }

        [RelayCommand]
        private void UpdateSelection(SelectableObject<Permission> permission)
        {
            HasChanges = true;
            SaveCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        protected void ClearInputs()
        {
            Name = null;
            foreach(SelectableObject<Permission> permission in Permissions)
            {
                permission.IsSelected = false;
            }
            HasChanges = false;
        }

        public override bool CanSave => base.CanSave && SelectedPermissions.Any();

        [ObservableProperty]
        IEnumerable<IGrouping<string, SelectableObject<Permission>>> _resources;

        [ObservableProperty]
        [Required(ErrorMessage = "الرجاء ادخال اسم الدور")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _name;

        partial void OnNameChanged(string oldValue, string newValue) => HasChanges = true;

        [ObservableProperty]
        private IEnumerable<SelectableObject<Permission>> _permissions;

        public IEnumerable<Permission> SelectedPermissions => Permissions.Where(x => x.IsSelected).Select(x => x.Value);

        [ObservableProperty]
        public IEnumerable<PermissionsGroup> _permissionsGroups;
        public int SelectedPermissionsCount => SelectedPermissions.Count();

        protected readonly IMediator _mediator;
        protected readonly IMessenger _messenger;
    }
}

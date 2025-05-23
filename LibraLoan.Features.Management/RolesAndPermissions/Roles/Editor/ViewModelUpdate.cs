using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.Roles.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Role role) : base(mediator, messenger)
        {
            Name = role.Name;
            _role = role;

            HasChanges = false;
        }

        public override string Title => "تعديل دور";

        protected override async Task LoadAsync(bool reload)
        {
            IEnumerable<Permission> permissions = (await _mediator.Send(new Core.Commands.Common.GetAllCommand<Permission>(reload)));

            Permissions = permissions.Select(x => new SelectableObject<Permission>(x)).ToList();

            IEnumerable<SelectableObject<Permission>> selectedPermissions = Permissions
                .Where(x => _role.Permissions.Any(y => y.Id == x.Value.Id))
                .ToList();

            foreach (SelectableObject<Permission> permission in selectedPermissions)
            {
                permission.IsSelected = true;
            }

            Resources = Permissions.GroupBy(x => x.Value.Resource.Name);
            PermissionsGroups = Resources.Select(x => new PermissionsGroup(x.Key, x.ToList() )).ToList();

        }

        private Role _role;
        protected override async Task Save()
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge("هل تريد حفظ التعديلات ؟"));
            if (!isConfirmed) return;

            RoleDto roleDto = new RoleDto(_role.Id, Name.Trim(), SelectedPermissions);
            bool isUpdated = await _mediator.Send(new Core.Commands.Common.UpdateCommand<RoleDto>(roleDto));

            if (isUpdated)
            {
                _role.Name = Name;
                _role.Permissions.Clear();

                foreach (Permission permission in SelectedPermissions)
                {
                    _role.Permissions.Add(permission);
                }
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم التحديث بنجاح"));
                HasChanges = false;
                return;
            }

            _messenger.Send(new Core.Messages.Common.ErrorMessage("هذا الدور موجود مسبقا"));
        }
    }
}

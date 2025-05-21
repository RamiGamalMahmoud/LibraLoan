using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.Roles.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
        }

        protected override async Task Save()
        {
            IEnumerable<Permission> selectedPermissions = Permissions.Where(x => x.IsSelected).Select(x => x.Value);
            RoleDto roleDto = new RoleDto(0, Name.Trim(), selectedPermissions);
            Role role = await _mediator.Send(new Core.Commands.Common.CreateCommand<Role, RoleDto>(roleDto));
            if(role is null)
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("هذا الدور موجود مسبقا"));
                return;
            }

            _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الحفظ بنجاح"));
            ClearInputs();
        }

        public override string Title => "إضافة دور";
    }
}

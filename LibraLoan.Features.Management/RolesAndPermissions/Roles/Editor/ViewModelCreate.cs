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
            RoleDto roleDto = new RoleDto(0, Name, selectedPermissions);
            await _mediator.Send(new Core.Commands.Common.CreateCommand<Role, RoleDto>(roleDto));
        }

        public override string Title => "إضافة دور";
    }
}

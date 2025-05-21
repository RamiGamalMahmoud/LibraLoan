using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.Permissions.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
        }

        public override string Title => "اضافة صلاحية";

        protected override async Task Save()
        {
            PermissionDto permissionDto = new PermissionDto(0, SelectedResource, SelectedAction);
            Permission permission = await _mediator.Send(new Core.Commands.Common.CreateCommand<Permission, PermissionDto>(permissionDto));
            if (permission is null)
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("هذه الصلاحية موجودة بالفعل"));
                return;
            }
            _messenger.Send(new Core.Messages.Common.SuccessMessage("تم اضافة الصلاحية بنجاح"));
            ClearInputs();
        }
    }
}

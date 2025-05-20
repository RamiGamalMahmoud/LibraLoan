using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.Permissions.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Permission permission) : base(mediator, messenger)
        {
            _permission = permission;
            SelectedResource = permission.Resource;
            SelectedAction = permission.Action;
            HasChanges = false;
        }

        private readonly Permission _permission;

        public override string Title => "تعديل الصلاحية";

        protected override async Task Save()
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge("هل تريد حفظ التعديلات ؟"));
            if (!isConfirmed) return;
            PermissionDto permissionDto = new PermissionDto(_permission.Id, SelectedResource, SelectedAction);
            bool isUpdated = await _mediator.Send(new Core.Commands.Common.UpdateCommand<PermissionDto>(permissionDto));
            _permission.Resource = SelectedResource;
            _permission.Action = SelectedAction;

            if (isUpdated)
            {
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم تعديل الصلاحية بنجاح"));
                return;
            }
            _messenger.Send(new Core.Messages.Common.ErrorMessage("فشل تعديل الصلاحية"));
        }
    }
}

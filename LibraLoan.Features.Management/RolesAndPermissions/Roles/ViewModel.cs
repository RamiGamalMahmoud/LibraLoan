using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using ActionModel = LibraLoan.Core.Models.Action;

namespace LibraLoan.Features.Management.RolesAndPermissions.Roles
{
    internal partial class ViewModel : ListingViewModelBase<Role>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger, appStateService)
        {
            _deleteMessage = "هل انت متأكد من حذف هذا الدور ؟";
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Role model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.DeleteAction);
        }

        protected override bool CanUpdate(Role model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.UpdateAction);
        }
    }
}

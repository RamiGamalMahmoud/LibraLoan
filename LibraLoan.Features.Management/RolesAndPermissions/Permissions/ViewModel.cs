using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using ActionModel = LibraLoan.Core.Models.Action;

namespace LibraLoan.Features.Management.RolesAndPermissions.Permissions
{
    internal partial class ViewModel : ListingViewModelBase<Permission>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService, System.IServiceProvider serviceProvider) : base(mediator, messenger, appStateService)
        {
            _serviceProvider = serviceProvider;
            _deleteMessage = "هل انت متأكد من حذف هذه الصلاحية؟";
        }

        protected override Task ShowCreate()
        {
            CloseEditor();
            Editor.ViewModelCreate viewModel = _serviceProvider.GetRequiredService<Editor.ViewModelCreate>();
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override Task ShowUpdate(Permission model)
        {
            CloseEditor();
            Editor.ViewModel viewModel = new Editor.ViewModelUpdate(_mediator, _messenger, model);
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Permission model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.DeleteAction);
        }

        protected override bool CanUpdate(Permission model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.UpdateAction);
        }
        private readonly System.IServiceProvider _serviceProvider;
    }
}

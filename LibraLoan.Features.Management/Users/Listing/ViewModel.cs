using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActionModel = LibraLoan.Core.Models.Action;

namespace LibraLoan.Features.Management.Users.Listing
{
    internal partial class ViewModel : ListingViewModelBase<User>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService, System.IServiceProvider serviceProvider) : base(mediator, messenger, appStateService)
        {
            _serviceProvider = serviceProvider;
            _deleteMessage = "هل انت متاكد من حذف هذا المستخدم؟";
        }

        protected override async Task LoadDataAsync(bool reload)
        {
            _users = await _mediator.Send(new Core.Commands.Common.GetAllCommand<User>(false));
            Models = _users;
        }

        protected override Task SearchAsync()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Models = _users;
            }

            else
            {
                Models = _users.Where(x => x.Username.Contains(SearchText)).ToList();
            }

            return Task.CompletedTask;
        }
        protected override Task ShowCreate()
        {
            CloseEditor();
            Editor.ViewModelCreate viewModel = _serviceProvider.GetRequiredService<Editor.ViewModelCreate>();
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override Task ShowUpdate(User model)
        {
            CloseEditor();
            IPasswordHasher passwordHasher = _serviceProvider.GetRequiredService<IPasswordHasher>();
            Editor.ViewModel viewModel = new Editor.ViewModelUpdate(_mediator, _messenger, passwordHasher, model);
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(User model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.DeleteAction) && !model.IsAdmin;
        }

        protected override bool CanUpdate(User model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.UpdateAction);
        }

        private IEnumerable<User> _users;
        private readonly System.IServiceProvider _serviceProvider;
    }
}

using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActionModel = LibraLoan.Core.Models.Action;

namespace LibraLoan.Features.Management.Users.Listing
{
    internal partial class ViewModel : ListingViewModelBase<User>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger, appStateService)
        {
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

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(User model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.DeleteAction);
        }

        protected override bool CanUpdate(User model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.ManagementResource, ActionModel.UpdateAction);
        }

        private IEnumerable<User> _users;
    }
}

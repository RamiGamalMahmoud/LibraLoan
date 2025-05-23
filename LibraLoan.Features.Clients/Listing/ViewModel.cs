using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Clients.Listing
{
    internal class ViewModel : ListingViewModelBase<Client>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger, appStateService)
        {
        }

        protected override Task SearchAsync()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Models = _tempModels;
            }
            else
            {
                Models = _tempModels
                    .Where(x => x.Name.Contains(SearchText) || x.Phone.Contains(SearchText) || x.Email is not null && x.Email.Contains(SearchText))
                    .ToList();
            }
            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.ClientsResource, Action.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Client model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.ClientsResource, Action.DeleteAction);
        }

        protected override bool CanUpdate(Client model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.ClientsResource, Action.UpdateAction);
        }
    }
}

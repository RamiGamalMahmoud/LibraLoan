using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using ActionModel = LibraLoan.Core.Models.Action;

namespace LibraLoan.Features.Publishers.Listing
{
    internal class ViewModel : ListingViewModelBase<Publisher>
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
                    .Where(
                       x => x.Name.Contains(SearchText) 
                    || x.Phone is not null && x.Phone.Contains(SearchText)
                    || x.Fax is not null && x.Fax.Contains(SearchText) 
                    || x.Email is not null && x.Email.Contains(SearchText) 
                    || x.Website is not null && x.Website.Contains(SearchText))
                    
                    .ToList();
            }

            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.PublishersResource, ActionModel.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Publisher model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.PublishersResource, ActionModel.DeleteAction);
        }

        protected override bool CanUpdate(Publisher model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.PublishersResource, ActionModel.UpdateAction);
        }
    }
}

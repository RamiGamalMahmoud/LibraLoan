using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.Listing
{
    internal partial class ViewModel : ListingViewModelBase<Author>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger, appStateService)
        {
        }

        protected override Task SearchAsync()
        {
            Models = _tempModels?.Where(x => x.Name.Contains(SearchText));

            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.AuthorsResource, Action.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Author model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.AuthorsResource, Action.DeleteAction);
        }

        protected override bool CanUpdate(Author model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.AuthorsResource, Action.UpdateAction);
        }
    }
}

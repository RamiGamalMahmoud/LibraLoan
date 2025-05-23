using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Books.Listing
{
    internal class ViewModel : ListingViewModelBase<Book>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger, appStateService)
        {
            _messenger.Register<Core.Messages.Books.BookReturnedMessage>(this, async (r, m) =>
            {
                await LoadDataAsync(true);
            });

            _messenger.Register<Core.Messages.Books.BookLoanedMessage>(this, async (r, m) =>
            {
                await LoadDataAsync(true);
            });
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
                    .Where(x => 
                    x.Title.Contains(SearchText)
                    || x.Isbn.Contains(SearchText)
                    || x.Authors.Any(a => a.Name.Contains(SearchText))
                    || x.Publisher.Name.Contains(SearchText)
                    )
                    .ToList();
            }
            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.BooksResource, Action.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Book model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.BooksResource, Action.DeleteAction);
        }

        protected override bool CanUpdate(Book model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.BooksResource, Action.UpdateAction);
        }
    }
}

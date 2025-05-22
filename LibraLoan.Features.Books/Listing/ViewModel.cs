using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Books.Listing
{
    internal class ViewModel : ListingViewModelBase<Book>
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
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
    }
}

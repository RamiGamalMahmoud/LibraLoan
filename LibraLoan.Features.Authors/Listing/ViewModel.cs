using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.Listing
{
    internal partial class ViewModel : ListingViewModelBase<Author>
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
        }

        protected override Task SearchAsync()
        {
            Models = _tempModels.Where(x => x.Name.Contains(SearchText));

            return Task.CompletedTask;
        }
    }
}

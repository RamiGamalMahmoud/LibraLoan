using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;

namespace LibraLoan.Features.Authors.Listing
{
    internal partial class ViewModel : ListingViewModelBase<Author>
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
        }
    }
}

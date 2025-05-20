using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;

namespace LibraLoan.Features.Clients.Listing
{
    internal class ViewModel : ListingViewModelBase<Client>
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
        }
    }
}

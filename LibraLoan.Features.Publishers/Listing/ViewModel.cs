using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;

namespace LibraLoan.Features.Publishers.Listing
{
    internal class ViewModel : ListingViewModelBase<Publisher>
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
        }
    }
}

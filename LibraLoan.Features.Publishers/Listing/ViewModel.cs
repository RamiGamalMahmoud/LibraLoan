using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Publishers.Listing
{
    internal class ViewModel : ListingViewModelBase<Publisher>
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
    }
}

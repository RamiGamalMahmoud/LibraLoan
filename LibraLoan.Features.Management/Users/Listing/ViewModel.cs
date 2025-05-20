using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.Users.Listing
{
    internal partial class ViewModel : ListingViewModelBase<User>
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
            _deleteMessage = "هل انت متاكد من حذف هذا المستخدم؟";
        }

        protected override async Task LoadDataAsync(bool reload)
        {
            _users = await _mediator.Send(new Core.Commands.Common.GetAllCommand<User>(false));
            Models = _users;
        }

        protected override bool CanDelete(User model)
        {
            return base.CanDelete(model) && !model.IsAdmin;
        }

        protected override Task SearchAsync()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Models = _users;
            }

            else
            {
                Models = _users.Where(x => x.Username.Contains(SearchText)).ToList();
            }

            return Task.CompletedTask;
        }

        private IEnumerable<User> _users;
    }
}

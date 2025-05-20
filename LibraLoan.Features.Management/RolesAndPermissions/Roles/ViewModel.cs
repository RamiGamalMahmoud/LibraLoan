using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;

namespace LibraLoan.Features.Management.RolesAndPermissions.Roles
{
    internal partial class ViewModel : ListingViewModelBase<Role>
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
            _deleteMessage = "هل انت متأكد من حذف هذا الدور ؟";
        }
    }
}

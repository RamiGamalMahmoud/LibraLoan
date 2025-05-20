using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;

namespace LibraLoan.Features.Management.RolesAndPermissions.Permissions
{
    internal partial class ViewModel : ListingViewModelBase<Permission>
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
            _deleteMessage = "هل انت متأكد من حذف هذه الصلاحية؟";
        }
    }
}

using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class ShowUpdatePermisssionCommandHandler(IMediator mediator, IMessenger messenger) : IRequestHandler<Core.Commands.Common.ShowUpdateCommand<Permission>>
    {
        public Task Handle(Common.ShowUpdateCommand<Permission> request, CancellationToken cancellationToken)
        {
            Permissions.Editor.ViewModel viewModel = new Permissions.Editor.ViewModelUpdate(mediator, messenger, request.Model);
            Permissions.Editor.View view = new Permissions.Editor.View(viewModel);
            return Task.CompletedTask;
        }
    }
}

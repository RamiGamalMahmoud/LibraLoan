using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using LibraLoan.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class ShowUpateRoleCommandHandler(IMediator mediator, IMessenger messenger) : IRequestHandler<Common.ShowUpdateCommand<Role>>
    {
        public Task Handle(Common.ShowUpdateCommand<Role> request, CancellationToken cancellationToken)
        {
            Roles.Editor.View view = new Roles.Editor.View(new Roles.Editor.ViewModelUpdate(mediator, messenger, request.Model));
            DialogWindow dialogWindow = new DialogWindow();
            dialogWindow.Content = view;
            dialogWindow.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

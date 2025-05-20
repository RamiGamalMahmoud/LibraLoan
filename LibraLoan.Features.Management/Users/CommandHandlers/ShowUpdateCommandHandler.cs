using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Management.Users.CommandHandlers
{
    internal class ShowUpdateCommandHandler(IMediator mediator, IMessenger messenger, IPasswordHasher passwordHasher) : IRequestHandler<ShowUpdateCommand<User>>
    {
        public Task Handle(ShowUpdateCommand<User> request, CancellationToken cancellationToken)
        {
            Editor.View view = new Editor.View(new Editor.ViewModelUpdate(mediator, messenger, passwordHasher, request.Model));
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

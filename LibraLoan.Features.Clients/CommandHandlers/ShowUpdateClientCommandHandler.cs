using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Models;
using LibraLoan.Features.Clients.Editor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Clients.CommandHandlers
{
    internal class ShowUpdateClientCommandHandler(IMediator mediator, IMessenger messenger) : IRequestHandler<ShowUpdateCommand<Client>>
    {
        public Task Handle(ShowUpdateCommand<Client> request, CancellationToken cancellationToken)
        {
            ViewModelUpdate viewModel = new ViewModelUpdate(mediator, messenger, request.Model);
            View view = new View(viewModel);
            //view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

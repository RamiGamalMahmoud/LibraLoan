using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Publishers.CommandHandlers
{
    internal class ShowUpdatePublisherCommandHandler(IMediator mediator, IMessenger messenger) : IRequestHandler<ShowUpdateCommand<Publisher>>
    {
        public Task Handle(ShowUpdateCommand<Publisher> request, CancellationToken cancellationToken)
        {
            Editor.ViewModel viewModel = new Editor.ViewModelUpdate(mediator, messenger, request.Model);
            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

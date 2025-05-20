using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.CommandHandler
{
    internal class ShowUpdateAuthorCommandHandler(IMediator mediator, IMessenger messenger) : IRequestHandler<Core.Commands.Common.ShowUpdateCommand<Author>>
    {
        public Task Handle(Common.ShowUpdateCommand<Author> request, CancellationToken cancellationToken)
        {
            Editor.ViewModel viewModel = new Editor.ViewModelUpdate(mediator, messenger, request.Model);
            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

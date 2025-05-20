using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Books.CommandHandlers
{
    internal class ShowUpdateBookCommandHandler(IMediator mediator, IMessenger messenger) : IRequestHandler<ShowUpdateCommand<Book>>
    {
        public Task Handle(ShowUpdateCommand<Book> request, CancellationToken cancellationToken)
        {
            Editor.View view = new Editor.View(new Editor.ViewModelUpdate(mediator, messenger, request.Model));
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

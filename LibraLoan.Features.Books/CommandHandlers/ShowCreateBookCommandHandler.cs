using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Books.CommandHandlers
{
    internal class ShowCreateBookCommandHandler(Editor.ViewModelCreate viewModel) : IRequestHandler<ShowCreateCommand<Book>>
    {
        public Task Handle(ShowCreateCommand<Book> request, CancellationToken cancellationToken)
        {
            Editor.View view = new Editor.View(viewModel);
            //view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

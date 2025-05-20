using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using LibraLoan.Features.Authors.Editor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.CommandHandler
{
    internal class ShowCreateAuthorCommandHandler(ViewModelCreate viewModel) : IRequestHandler<Core.Commands.Common.ShowCreateCommand<Author>>
    {
        public Task Handle(Common.ShowCreateCommand<Author> request, CancellationToken cancellationToken)
        {
            View view = new View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

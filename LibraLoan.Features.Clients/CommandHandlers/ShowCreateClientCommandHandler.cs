using LibraLoan.Core.Models;
using LibraLoan.Features.Clients.Editor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Clients.CommandHandlers
{
    internal class ShowCreateClientCommandHandler(ViewModelCreate viewModel) : IRequestHandler<ShowCreateCommand<Client>>
    {
        public Task Handle(ShowCreateCommand<Client> request, CancellationToken cancellationToken)
        {
            View view = new View(viewModel);
            //view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

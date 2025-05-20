using LibraLoan.Core.Models;
using LibraLoan.Features.Publishers.Editor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Publishers.CommandHandlers
{
    internal class ShowCreatePublisherCommandHandler(ViewModelCreate viewModelCreate) : IRequestHandler<ShowCreateCommand<Publisher>>
    {
        public Task Handle(ShowCreateCommand<Publisher> request, CancellationToken cancellationToken)
        {
            View view = new View(viewModelCreate);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

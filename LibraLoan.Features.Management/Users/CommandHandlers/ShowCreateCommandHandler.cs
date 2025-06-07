using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using LibraLoan.Features.Management.Users.Editor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.Users.CommandHandlers
{
    internal class ShowCreateCommandHandler(ViewModelCreate viewModel) : IRequestHandler<Core.Commands.Common.ShowCreateCommand<User>>
    {
        public Task Handle(Common.ShowCreateCommand<User> request, CancellationToken cancellationToken)
        {
            View view = new View(viewModel);
            return Task.CompletedTask;
        }
    }
}

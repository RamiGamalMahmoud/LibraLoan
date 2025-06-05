using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Clients.CommandHandlers
{
    internal class DeleteClientCommandHandler(IClientsRepository repository) : IRequestHandler<Core.Commands.Common.DeleteCommand<Client>, bool>
    {
        public async Task<bool> Handle(Common.DeleteCommand<Client> request, CancellationToken cancellationToken)
        {
            return await repository.DeleteAsync(request.Model);
        }
    }
}

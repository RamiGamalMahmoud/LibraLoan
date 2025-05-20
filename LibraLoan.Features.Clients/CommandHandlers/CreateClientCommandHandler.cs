using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Clients.CommandHandlers
{
    internal class CreateClientCommandHandler(IClientsRepository repository) : IRequestHandler<CreateCommand<Client, ClientDto>, Client>
    {
        public async Task<Client> Handle(CreateCommand<Client, ClientDto> request, CancellationToken cancellationToken)
        {
            return await repository.CreateAsync(request.Model);
        }
    }
}

using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Clients.CommandHandlers
{
    internal class UpdateClientCommandHandler(IClientsRepository repository) : IRequestHandler<UpdateCommand<ClientDto>, bool>
    {
        public async Task<bool> Handle(UpdateCommand<ClientDto> request, CancellationToken cancellationToken)
        {
            return await repository.UpdateAsync(request.Model);
        }
    }
}

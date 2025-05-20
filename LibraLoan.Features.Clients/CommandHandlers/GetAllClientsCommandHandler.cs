using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Clients.CommandHandlers
{
    internal class GetAllClientsCommandHandler(IClientsRepository repository) : IRequestHandler<GetAllCommand<Client>, IEnumerable<Client>>
    {
        public async Task<IEnumerable<Client>> Handle(GetAllCommand<Client> request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync(request.Reload);
        }
    }
}

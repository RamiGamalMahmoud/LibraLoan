using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Publishers.CommandHandlers
{
    internal class GetAllPublishersCommandHandler(IPublishersRepository publishersRepository) : IRequestHandler<GetAllCommand<Publisher>, IEnumerable<Publisher>>
    {
        public async Task<IEnumerable<Publisher>> Handle(GetAllCommand<Publisher> request, CancellationToken cancellationToken)
        {
            return await publishersRepository.GetAllAsync(request.Reload);
        }
    }
}

using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.CommandHandler
{
    internal class GetAllAuthorsCommandHandler(IAuthorsRepository authorsRepository) : IRequestHandler<Core.Commands.Common.GetAllCommand<Author>, IEnumerable<Author>>
    {
        public async Task<IEnumerable<Author>> Handle(Common.GetAllCommand<Author> request, CancellationToken cancellationToken)
        {
            return await authorsRepository.GetAllAsync(request.Reload);
        }
    }
}

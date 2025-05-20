using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Loans.CommandHandlers
{
    internal class GetAllLoansCommandHandler(ILoansRepository repository) : IRequestHandler<GetAllCommand<Loan>, IEnumerable<Loan>>
    {
        public async Task<IEnumerable<Loan>> Handle(GetAllCommand<Loan> request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync(request.Reload);
        }
    }
}

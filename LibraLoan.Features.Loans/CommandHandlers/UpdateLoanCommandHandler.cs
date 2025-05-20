using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Loans.CommandHandlers
{
    internal class UpdateLoanCommandHandler(ILoansRepository repository) : IRequestHandler<UpdateCommand<LoanDto>, bool>
    {
        public async Task<bool> Handle(UpdateCommand<LoanDto> request, CancellationToken cancellationToken)
        {
            return await repository.UpdateAsync(request.Model);
        }
    }
}

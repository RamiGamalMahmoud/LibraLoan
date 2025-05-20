using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Loans.CommandHandlers
{
    internal class CreateLoanCommandHandler(ILoansRepository repository) : IRequestHandler<CreateCommand<Loan, LoanDto>, Loan>
    {
        public async Task<Loan> Handle(CreateCommand<Loan, LoanDto> request, CancellationToken cancellationToken)
        {
            return await repository.CreateAsync(request.Model);
        }
    }
}

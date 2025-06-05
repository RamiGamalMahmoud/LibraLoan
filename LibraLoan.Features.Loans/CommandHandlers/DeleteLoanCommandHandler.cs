using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Loans.CommandHandlers
{
    internal class DeleteLoanCommandHandler(ILoansRepository repository) : IRequestHandler<Core.Commands.Common.DeleteCommand<Loan>, bool>
    {
        public async Task<bool> Handle(Common.DeleteCommand<Loan> request, CancellationToken cancellationToken)
        {
            return await repository.DeleteAsync(request.Model);
        }
    }
}

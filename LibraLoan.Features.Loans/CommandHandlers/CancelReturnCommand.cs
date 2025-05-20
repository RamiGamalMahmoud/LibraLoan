using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Loans.CommandHandlers
{
    internal static class CancelReturnCommand
    {
        internal record Command(Loan Loan) : IRequest;

        internal class Handler(ILoansRepository loansRepository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await loansRepository.CancelReturn(request.Loan);
            }
        }
    }
}

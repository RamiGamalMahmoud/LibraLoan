using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Loans.CommandHandlers
{
    internal static class ReturnBookCommand
    {
        internal record Command(Loan Loan, DateTime ReturnDate) : IRequest;

        internal class Handler(ILoansRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await repository.ReturnBook(request.Loan, request.ReturnDate);
            }
        }
    }
}

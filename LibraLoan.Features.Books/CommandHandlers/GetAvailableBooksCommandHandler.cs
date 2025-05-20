using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Books;

namespace LibraLoan.Features.Books.CommandHandlers
{
    internal class GetAvailableBooksCommandHandler(IBooksRepository repository) : IRequestHandler<GetAvailableBooksCommand, IEnumerable<Book>>
    {
        public async Task<IEnumerable<Book>> Handle(GetAvailableBooksCommand request, CancellationToken cancellationToken)
        {
            return await repository.GetAvailableBooks();
        }
    }
}

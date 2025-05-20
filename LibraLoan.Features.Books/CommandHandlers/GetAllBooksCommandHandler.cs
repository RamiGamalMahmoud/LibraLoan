using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Books.CommandHandlers
{
    internal class GetAllBooksCommandHandler(IBooksRepository booksRepository) : IRequestHandler<GetAllCommand<Book>, IEnumerable<Book>>
    {
        public async Task<IEnumerable<Book>> Handle(GetAllCommand<Book> request, CancellationToken cancellationToken)
        {
            return await booksRepository.GetAllAsync(request.Reload);
        }
    }
}

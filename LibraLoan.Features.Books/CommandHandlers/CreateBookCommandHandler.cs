using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Books.CommandHandlers
{
    internal class CreateBookCommandHandler(IBooksRepository booksRepository) : IRequestHandler<CreateCommand<Book, BookDto>, Book>
    {
        public async Task<Book> Handle(CreateCommand<Book, BookDto> request, CancellationToken cancellationToken)
        {
            return await booksRepository.CreateAsync(request.Model);
        }
    }
}

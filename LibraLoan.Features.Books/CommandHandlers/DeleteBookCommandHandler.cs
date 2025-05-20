using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Books.CommandHandlers
{
    internal class DeleteBookCommandHandler(IBooksRepository booksRepository) : IRequestHandler<DeleteCommand<Book>, bool>
    {
        public async Task<bool> Handle(DeleteCommand<Book> request, CancellationToken cancellationToken)
        {
            return await booksRepository.DeleteAsync(request.Model);
        }
    }
}

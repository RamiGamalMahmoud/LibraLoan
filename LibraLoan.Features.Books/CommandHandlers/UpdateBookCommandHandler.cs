using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Books.CommandHandlers
{
    internal class UpdateBookCommandHandler(IBooksRepository booksRepository) : IRequestHandler<UpdateCommand<BookDto>, bool>
    {
        public async Task<bool> Handle(UpdateCommand<BookDto> request, CancellationToken cancellationToken)
        {
            return await booksRepository.UpdateAsync(request.Model);
        }
    }
}

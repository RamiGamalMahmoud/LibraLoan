using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.CommandHandler
{
    internal class CreateAuthorCommandHandler(IAuthorsRepository authorsRepository) : IRequestHandler<Core.Commands.Common.CreateCommand<Author, AuthorDto>, Author>
    {
        public async Task<Author> Handle(Common.CreateCommand<Author, AuthorDto> request, CancellationToken cancellationToken)
        {
            return await authorsRepository.CreateAsync(request.Model);
        }
    }
}

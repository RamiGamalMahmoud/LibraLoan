using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.CommandHandler
{
    internal class UpdateAuthorCommandHandler(IAuthorsRepository authorsRepository) : IRequestHandler<Core.Commands.Common.UpdateCommand<AuthorDto>, bool>
    {
        public async Task<bool> Handle(Common.UpdateCommand<AuthorDto> request, CancellationToken cancellationToken)
        {
            return await authorsRepository.UpdateAsync(request.Model);
        }
    }
}

using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.CommandHandler
{
    internal class DeleteAuthorCommandHandler(IAuthorsRepository authorsRepository) : IRequestHandler<Core.Commands.Common.DeleteCommand<Author>, bool>
    {
        public async Task<bool> Handle(Common.DeleteCommand<Author> request, CancellationToken cancellationToken)
        {
            return await authorsRepository.DeleteAsync(request.Model);
        }
    }
}

using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.Users.CommandHandlers
{
    internal class DeleteUserCommandHandler(IUsersRepository usersRepository) : IRequestHandler<Core.Commands.Common.DeleteCommand<User>, bool>
    {
        public async Task<bool> Handle(Common.DeleteCommand<User> request, CancellationToken cancellationToken)
        {
            return await usersRepository.DeleteAsync(request.Model);
        }
    }
}

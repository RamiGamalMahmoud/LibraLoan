using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.Users.CommandHandlers
{
    internal class CreateUserComandHandler(IUsersRepository usersRepository) : IRequestHandler<Core.Commands.Common.CreateCommand<User, UserDto>, User>
    {
        public async Task<User> Handle(Common.CreateCommand<User, UserDto> request, CancellationToken cancellationToken)
        {
            return await usersRepository.CreateAsync(request.Model);
        }
    }
}

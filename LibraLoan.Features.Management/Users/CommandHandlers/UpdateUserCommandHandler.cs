using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.Users.CommandHandlers
{
    internal class UpdateUserCommandHandler(IUsersRepository usersRepository) : IRequestHandler<Core.Commands.Common.UpdateCommand<UserDto>, bool>
    {
        public async Task<bool> Handle(Common.UpdateCommand<UserDto> request, CancellationToken cancellationToken)
        {
            return await usersRepository.UpdateAsync(request.Model);
        }
    }
}

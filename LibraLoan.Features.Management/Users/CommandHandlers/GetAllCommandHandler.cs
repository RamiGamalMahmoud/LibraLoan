using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.Users.CommandHandlers
{
    internal class GetAllCommandHandler(IUsersRepository usersRepository) : IRequestHandler<Core.Commands.Common.GetAllCommand<User>, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> Handle(Common.GetAllCommand<User> request, CancellationToken cancellationToken)
        {
            return await usersRepository.GetAllAsync(request.Reload);
        }
    }
}

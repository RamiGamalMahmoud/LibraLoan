using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class GetAllRolesCommandHandler(IRolesRepository rolesRepository) : IRequestHandler<Common.GetAllCommand<Role>, IEnumerable<Role>>
    {
        public async Task<IEnumerable<Role>> Handle(Common.GetAllCommand<Role> request, CancellationToken cancellationToken)
        {
            return await rolesRepository.GetAllAsync(request.Reload);
        }
    }
}

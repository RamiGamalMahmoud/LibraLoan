using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class UpdateRoleCommandHandler(IRolesRepository rolesRepository) : IRequestHandler<Common.UpdateCommand<RoleDto>, bool>
    {
        public async Task<bool> Handle(Common.UpdateCommand<RoleDto> request, CancellationToken cancellationToken)
        {
            return await rolesRepository.UpdateAsync(request.Model);
        }
    }
}

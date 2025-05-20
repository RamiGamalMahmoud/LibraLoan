using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class CreateRoleCommandHandler(IRolesRepository rolesRepository) : IRequestHandler<Common.CreateCommand<Role, RoleDto>, Role>
    {
        public async Task<Role> Handle(Common.CreateCommand<Role, RoleDto> request, CancellationToken cancellationToken)
        {
            return await rolesRepository.CreateAsync(request.Model);
        }
    }
}

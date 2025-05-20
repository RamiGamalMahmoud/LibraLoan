using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class CreatePermissionCommandHandler(IPermissionsRepository permissionsRepository) : IRequestHandler<Common.CreateCommand<Permission, PermissionDto>, Permission>
    {
        public async Task<Permission> Handle(Common.CreateCommand<Permission, PermissionDto> request, CancellationToken cancellationToken)
        {
            return await permissionsRepository.CreateAsync(request.Model);
        }
    }
}

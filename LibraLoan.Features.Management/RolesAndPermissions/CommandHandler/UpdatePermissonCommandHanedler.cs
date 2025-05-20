using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class UpdatePermissonCommandHanedler(IPermissionsRepository permissionsRepository) : IRequestHandler<Core.Commands.Common.UpdateCommand<PermissionDto>, bool>
    {
        public async Task<bool> Handle(Common.UpdateCommand<PermissionDto> request, CancellationToken cancellationToken)
        {
            return await permissionsRepository.UpdateAsync(request.Model);
        }
    }
}

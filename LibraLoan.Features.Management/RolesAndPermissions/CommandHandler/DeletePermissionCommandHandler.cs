using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class DeletePermissionCommandHandler(IPermissionsRepository permissionsRepository) : IRequestHandler<Common.DeleteCommand<Permission>, bool>
    {
        public async Task<bool> Handle(Common.DeleteCommand<Permission> request, CancellationToken cancellationToken)
        {
            return await permissionsRepository.DeleteAsync(request.Model);
        }
    }
}

using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class GetAllPermissionsCommandHandler(IPermissionsRepository permissionsRepository) : IRequestHandler<Common.GetAllCommand<Permission>, IEnumerable<Permission>>
    {
        public async Task<IEnumerable<Permission>> Handle(Common.GetAllCommand<Permission> request, CancellationToken cancellationToken)
        {
            return await permissionsRepository.GetAllAsync(request.Reload);
        }
    }
}

using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{

    internal class GetAllResourcesCommandHandler(IPermissionsRepository permissionsRepository) :
        IRequestHandler<Common.GetAllCommand<Resource>, IEnumerable<Resource>>
    {
        public async Task<IEnumerable<Resource>> Handle(Common.GetAllCommand<Resource> request, CancellationToken cancellationToken)
        {
            return await permissionsRepository.GetAllResources();
        }
    }
}

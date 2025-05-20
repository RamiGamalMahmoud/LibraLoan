using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class GetAllActionsCommandHandler(IPermissionsRepository permissionsRepository) :
        IRequestHandler<Common.GetAllCommand<Action>, IEnumerable<Action>>
    {
        public async Task<IEnumerable<Action>> Handle(Common.GetAllCommand<Action> request, CancellationToken cancellationToken)
        {
            return await permissionsRepository.GetAllActions();
        }
    }
}

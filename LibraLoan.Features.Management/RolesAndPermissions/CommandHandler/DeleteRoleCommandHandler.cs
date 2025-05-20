using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class DeleteRoleCommandHandler(IRolesRepository rolesRepository) : IRequestHandler<Common.DeleteCommand<Role>, bool>
    {
        public async Task<bool> Handle(Common.DeleteCommand<Role> request, CancellationToken cancellationToken)
        {
            return await rolesRepository.DeleteAsync(request.Model);
        }
    }
}

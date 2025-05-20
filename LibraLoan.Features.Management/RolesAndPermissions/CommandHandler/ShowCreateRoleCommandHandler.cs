using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using LibraLoan.Features.Management.RolesAndPermissions.Roles.Editor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class ShowCreateRoleCommandHandler(ViewModelCreate viewModel) : IRequestHandler<Common.ShowCreateCommand<Role>>
    {
        public Task Handle(Common.ShowCreateCommand<Role> request, CancellationToken cancellationToken)
        {
            Roles.Editor.View view = new Roles.Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

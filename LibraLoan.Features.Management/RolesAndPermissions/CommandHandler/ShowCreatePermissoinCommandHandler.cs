using LibraLoan.Core.Commands;
using LibraLoan.Core.Models;
using LibraLoan.Features.Management.RolesAndPermissions.Permissions.Editor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraLoan.Features.Management.RolesAndPermissions.CommandHandler
{
    internal class ShowCreatePermissoinCommandHandler(ViewModelCreate viewModel) : IRequestHandler<Common.ShowCreateCommand<Permission>>
    {
        public Task Handle(Common.ShowCreateCommand<Permission> request, CancellationToken cancellationToken)
        {
            Permissions.Editor.View view = new Permissions.Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

using LibraLoan.Core.Models;
using LibraLoan.Features.Loans.Editor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Loans.CommandHandlers
{
    internal class ShowCreateLoanCommandHandler(ViewModelCreate viewModel) : IRequestHandler<ShowCreateCommand<Loan>>
    {
        public Task Handle(ShowCreateCommand<Loan> request, CancellationToken cancellationToken)
        {
            View view = new View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}

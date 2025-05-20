using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Models;
using LibraLoan.Features.Loans.Editor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Loans.CommandHandlers
{
    internal class ShowUpdateLoanCommandHandler(IMediator mediator, IMessenger messenger) : IRequestHandler<ShowUpdateCommand<Loan>>
    {
        public Task Handle(ShowUpdateCommand<Loan> request, CancellationToken cancellationToken)
        {
            ViewModelUpdate viewModel = new ViewModelUpdate(mediator, messenger, request.Model);
            View view = new View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}


using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Loans.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger)
        {
            _appStateService = appStateService;
        }

        public override string Title => "إنشاء استعارة جديدة";

        protected override async Task Save()
        {
            LoanDto loanDto = new LoanDto(0, SelectedBook, SelectedClient, DateTime.Now, (DateTime)ExpectedReturnDate, ActualReturnDate, _appStateService.CurrentUser);
            Loan loan = await _mediator.Send(new CreateCommand<Loan, LoanDto>(loanDto));
            if (loan is not null)
            {
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الحفظ بنجاح"));
                HasChanges = false;
            }
            else
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("حدث خطأ في الحفظ"));
            }
        }

        private readonly IAppStateService _appStateService;
    }
}

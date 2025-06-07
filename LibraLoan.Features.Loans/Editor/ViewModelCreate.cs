
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
            DateTime loanDate = (DateTime)LoanDate;
            DateTime expectedReturnDate = (DateTime)ExpectedReturnDate;
            DateTime? actualReturnDate = ActualReturnDate;

            LoanDto loanDto = new LoanDto(0, SelectedBook, SelectedClient, loanDate.Date, expectedReturnDate, actualReturnDate, _appStateService.CurrentUser);
            Loan loan = await _mediator.Send(new CreateCommand<Loan, LoanDto>(loanDto));
            if (loan is null)
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("حدث خطأ في الحفظ"));
                return;
            }

            _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الحفظ بنجاح"));
            _messenger.Send(new Core.Messages.Books.BookLoanedMessage(loan.Book.Id));
            ClearInputs();
            HasChanges = false;
        }

        private readonly IAppStateService _appStateService;
    }
}

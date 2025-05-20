
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Loans.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Loan model) : base(mediator, messenger)
        {
            _loan = model;

            SelectedBook = model.Book;
            SelectedClient = model.Client;
            ExpectedReturnDate = model.ExpectedReturnDate;
            LoanDate = model.LoanDate;
            IsReturned = model.IsReturned;
            ActualReturnDate = model.ActualReturnDate;
            Price = model.Price;
            HasChanges = false;
        }

        public override string Title => "تعديل بيانات استعارة";

        protected override async Task Save()
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge("هل تريد حفظ التعديلات ؟"));
            if (!isConfirmed) return;

            LoanDto loanDto = new LoanDto(_loan.Id, SelectedBook, SelectedClient, DateTime.Now, (DateTime)ExpectedReturnDate, ActualReturnDate, null);


            bool isUpdated = await _mediator.Send(new UpdateCommand<LoanDto>(loanDto));
            if (isUpdated)
            {
                _loan.Book = SelectedBook;
                _loan.Client = SelectedClient;
                _loan.ExpectedReturnDate = (DateTime)ExpectedReturnDate;
                _loan.LoanDate = DateTime.Now;
                if(ActualReturnDate is not null)
                {
                    _loan.ActualReturnDate = ActualReturnDate;
                }
                _loan.Price = Price;
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم التعديل بنجاح"));
                HasChanges = false;
            }
            else
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("حدث خطأ في التعديل"));
            }

        }

        private readonly Loan _loan;
    }
}

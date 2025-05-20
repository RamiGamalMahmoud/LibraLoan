using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System;
using System.Threading.Tasks;

namespace LibraLoan.Features.Loans.Listing
{
    internal partial class ViewModel : ListingViewModelBase<Loan>
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger)
        {
        }

        [RelayCommand]
        private async Task ReturnBook(Loan loan)
        {
            if (!_messenger.Send(new Core.Messages.Common.ConfigrRequestMessge($"هل تريد ارجاع الكتاب : {loan.Book.Title}؟"))) return;
            try
            {
                await _mediator.Send(new CommandHandlers.ReturnBookCommand.Command(loan, DateTime.Now));
                loan.Return();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [RelayCommand]
        private async Task CancelReturn(Loan model)
        {
            if (!_messenger.Send(new Core.Messages.Common.ConfigrRequestMessge($"هل تريد إلغاء ارجاع الكتاب : {model.Book.Title}؟"))) return;
            await _mediator.Send(new CommandHandlers.CancelReturnCommand.Command(model));
            model.CancelReturn();
        }
    }
}

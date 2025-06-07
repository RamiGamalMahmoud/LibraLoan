using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using ActionModel = LibraLoan.Core.Models.Action;

namespace LibraLoan.Features.Loans.Listing
{
    internal partial class ViewModel : ListingViewModelBase<Loan>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService, System.IServiceProvider serviceProvider) : base(mediator, messenger, appStateService)
        {
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        private async Task ReturnBook(Loan loan)
        {
            DateWindow dateWindow = new DateWindow(loan.Book.Title, loan.LoanDate, loan.ExpectedReturnDate);
            if(dateWindow.ShowDialog() is false) return;

            DateTime selectedDate = dateWindow.viewModel.SelectedDate;

            if(selectedDate < loan.LoanDate)
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("لا يمكن ارجاع الكتاب في تاريخ يسبق تاريخ الاستعارة"));
                return;
            }

            try
            {
                await _mediator.Send(new CommandHandlers.ReturnBookCommand.Command(loan, (DateTime)selectedDate));
                _messenger.Send(new Core.Messages.Books.BookLoanedMessage(loan.Book.Id));
                _messenger.Send(new Core.Messages.Common.SuccessMessage($"تم ارجاع الكتاب : {loan.Book.Title}"));
                loan.Return(selectedDate);
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
            _messenger.Send(new Core.Messages.Books.BookReturnedMessage(model.Book.Id));
            _messenger.Send(new Core.Messages.Common.SuccessMessage($"تم إلغاء ارجاع الكتاب : {model.Book.Title}"));
        }

        protected override Task SearchAsync()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Models = _tempModels;
            }
            else
            {
                Models = _tempModels
                    .Where(x => x.Book.Title.Contains(SearchText) || x.Client.Name.Contains(SearchText) || x.Book.Isbn.Contains(SearchText))
                    .ToList();
            }
            return Task.CompletedTask;
        }

        protected override Task ShowCreate()
        {
            CloseEditor();
            Editor.ViewModelCreate viewModel = _serviceProvider.GetRequiredService<Editor.ViewModelCreate>();
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override Task ShowUpdate(Loan model)
        {
            CloseEditor();
            Editor.ViewModel viewModel = new Editor.ViewModelUpdate(_mediator, _messenger, model);
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.LoansResource, ActionModel.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Loan model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.LoansResource, ActionModel.DeleteAction);
        }

        protected override bool CanUpdate(Loan model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.LoansResource, ActionModel.UpdateAction);
        }
        private readonly System.IServiceProvider _serviceProvider;
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraLoan.Features.Loans.Editor
{
    internal abstract partial class ViewModel : EditorViewModelBase
    {
        protected ViewModel(IMediator mediator, IMessenger messenger) : base()
        {
            _mediator = mediator;
            _messenger = messenger;

            _notifyPropertiesNames =
                [
                nameof(SelectedBook),
                nameof(SelectedClient),
                nameof(ExpectedReturnDate),
                nameof(LoanDate),
                nameof(IsReturned),
                nameof(ActualReturnDate),
                nameof(Price)
                ];
        }

        public async Task LoadAsync()
        {
            Books = await _mediator.Send(new Core.Commands.Books.GetAvailableBooksCommand());
            Clients = await _mediator.Send(new Core.Commands.Common.GetAllCommand<Client>(false));
        }
        public new bool HasErrors => base.HasErrors || !IsValidActualReturnDate || !IsValidExpectedReturnDate;
        public string Errors
        {
            get
            {
                StringBuilder errors = new StringBuilder();

                if (!IsValidActualReturnDate)
                {
                    errors.AppendLine("لا يمكن للتاريخ الفعلي لإرجاع الكتاب أن يسبق تاريخ الاستعارة");
                }

                if (!IsValidExpectedReturnDate)
                {
                    errors.AppendLine("لا يمكن للتاريخ المتوقع لإرجاع الكتاب أن يسبق تاريخ الاستعارة");
                }

                return errors.ToString();
            }
        }

        public bool IsValidActualReturnDate => ActualReturnDate is null || LoanDate is not null && ActualReturnDate.Value.Date >= LoanDate.Value.Date;

        public bool IsValidExpectedReturnDate => ExpectedReturnDate.HasValue && ExpectedReturnDate.Value.Date >= LoanDate.Value.Date;

        public override bool CanSave => base.CanSave && IsValidActualReturnDate && IsValidExpectedReturnDate;

        [ObservableProperty]
        private IEnumerable<Book> _books;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب اختيار الكتاب")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private Book _selectedBook;

        [ObservableProperty]
        private IEnumerable<Client> _clients;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب اختيار العميل")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private Client _selectedClient;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال التاريخ المتوقع لإرجاع الكتاب")]
        [NotifyPropertyChangedFor(nameof(IsValidExpectedReturnDate))]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyPropertyChangedFor(nameof(Errors))]
        private DateTime? _expectedReturnDate;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب إدخال تاريخ الاستعارة")]
        [NotifyPropertyChangedFor(nameof(IsValidExpectedReturnDate))]
        [NotifyPropertyChangedFor(nameof(IsValidActualReturnDate))]
        [NotifyPropertyChangedFor(nameof(Errors))]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private DateTime? _loanDate = DateTime.Now;

        [ObservableProperty]
        private double _price;

        [ObservableProperty]
        private bool? _isReturned;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValidActualReturnDate))]
        [NotifyPropertyChangedFor(nameof(Errors))]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private DateTime? _actualReturnDate;

        [RelayCommand]
        protected void ClearInputs()
        {
            SelectedBook = null;
            SelectedClient = null;
            ExpectedReturnDate = null;
            ActualReturnDate = null;
            LoanDate = null;
        }

        protected readonly IMediator _mediator;
        protected readonly IMessenger _messenger;
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace LibraLoan.Features.Loans.Listing
{
    [ObservableObject]
    internal partial class DateWindow : Window
    {
        public DateWindow(string bookTitle, DateTime loanDate, DateTime expectedReturnDate)
        {
            InitializeComponent();
            viewModel = new DateWindowViewModel(bookTitle, loanDate, expectedReturnDate);
            DataContext = viewModel;
        }

        public DateWindowViewModel viewModel { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

    internal partial class DateWindowViewModel : ObservableValidator
    {
        public string Message { get; }
        public DateTime? CommpareDate { get; }

        public bool HasMessage => !string.IsNullOrEmpty(Message);


        [ObservableProperty]
        [Required]
        [NotifyDataErrorInfo]
        private DateTime? _selectedDate = DateTime.Today;

        public DateWindowViewModel(string bookTitle, DateTime loanDate, DateTime expectedReturnDate)
        {
            Message = "هل تريد ارجاع الكتاب التالي ؟";
            BookTitle = bookTitle;
            LoanDate = loanDate;
            ExpectedReturnDate = expectedReturnDate;
            OnPropertyChanged(nameof(BookTitle));
            OnPropertyChanged(nameof(LoanDate));
            OnPropertyChanged(nameof(ExpectedReturnDate));
        }

        public string BookTitle { get; }
        public DateTime LoanDate { get; }
        public DateTime ExpectedReturnDate { get; }
    }
}

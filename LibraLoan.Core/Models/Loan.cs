using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Loan : ModelBase
    {
        public int BookId { get; set; }
        [ObservableProperty]
        private Book _book;

        public int ClientId { get; set; }
        [ObservableProperty]
        private Client _client;

        [ObservableProperty]
        private DateTime _loanDate;

        [ObservableProperty]
        private double _price;

        [ObservableProperty]
        private DateTime _expectedReturnDate;

        [ObservableProperty]
        private bool _isReturned;

        public int CreatedById { get; set; }
        [ObservableProperty]
        private User _createdBy;

        [ObservableProperty]
        private DateTime? _actualReturnDate;

        public void Return(DateTime? actualReturnDate = null)
        {
            IsReturned = true;
            ActualReturnDate = actualReturnDate ?? DateTime.Now;
        }

        public void CancelReturn()
        {
            IsReturned = false;
            ActualReturnDate = null;
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Book : ModelBase
    {
        [ObservableProperty]
        private byte[] _photo;

        [ObservableProperty]
        private string _isbn;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private int? _version;

        [ObservableProperty]
        private DateTime _publishDate;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsAvailable))]
        [NotifyPropertyChangedFor(nameof(AvailableCopies))]
        private int _copies = 1;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsAvailable))]
        [NotifyPropertyChangedFor(nameof(AvailableCopies))]
        private int _loanedCopies = 0;

        public bool IsAvailable => Copies - LoanedCopies > 0;
        public int AvailableCopies => Copies - LoanedCopies;
        public int PublisherId { get; set; }
        [ObservableProperty]
        private Publisher _publisher;

        public int CreatedById { get; set; }
        [ObservableProperty]
        private User _createdBy;
        
        public ICollection<Author> Authors { get; } = new ObservableCollection<Author>();
        public ICollection<Loan> Loans { get; } = new ObservableCollection<Loan>();
    }
}

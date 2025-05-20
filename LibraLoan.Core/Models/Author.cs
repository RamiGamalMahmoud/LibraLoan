using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Author : ModelBase
    {
        [ObservableProperty]
        private string _name;

        public int CreatedById { get; set; }
        [ObservableProperty]
        private User _createdBy;

        public ICollection<Book> Books { get; } = new ObservableCollection<Book>();
    }
}

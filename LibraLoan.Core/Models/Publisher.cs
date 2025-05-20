using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Publisher : ModelBase
    {
        [ObservableProperty]
        private string _name;
        
        [ObservableProperty]
        private string _phone;
        
        [ObservableProperty]
        private string _fax;
        
        [ObservableProperty]
        private string _email;
        
        [ObservableProperty]
        private string _website;

        public int CreatedById { get; set; }
        [ObservableProperty]
        private User _createdBy;

        public ObservableCollection<Book> Books => new ObservableCollection<Book>();

    }
}

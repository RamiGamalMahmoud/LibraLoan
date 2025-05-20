using CommunityToolkit.Mvvm.ComponentModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Client : ModelBase
    {

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _phone;

        [ObservableProperty]
        private string _email;

        public int CreatedById { get; set; }
        [ObservableProperty]
        private User _createdBy;
    }
}


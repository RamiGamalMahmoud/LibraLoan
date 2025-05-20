using CommunityToolkit.Mvvm.ComponentModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Resource : ModelBase
    {
        [ObservableProperty]
        private string _name;
    }
}

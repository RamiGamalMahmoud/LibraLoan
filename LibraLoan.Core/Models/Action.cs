using CommunityToolkit.Mvvm.ComponentModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Action : ModelBase
    {
        [ObservableProperty]
        private string _name;
    }
}

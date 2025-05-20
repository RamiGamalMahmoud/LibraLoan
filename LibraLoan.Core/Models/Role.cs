using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Role : ModelBase
    {
        [ObservableProperty]
        private string _name;

        public ICollection<Permission> Permissions { get; } = new ObservableCollection<Permission>();
    }
}

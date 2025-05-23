using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class User : ModelBase
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private bool _isActive;

        public int? CreatedById { get; set; }
        [ObservableProperty]
        private User _createdBy;

        public int? RoleId { get; set; }
        [ObservableProperty]
        private Role _role;

        public bool IsAdmin { get; private set; }

        public bool HasPermission(string resource, string action)
        {
            return IsAdmin || Role.Permissions.Any(x => x.Resource.Name == resource && x.Action.Name == action);
        }
    }
}

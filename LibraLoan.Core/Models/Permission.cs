using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Permission : ModelBase
    {
        //[ObservableProperty]
        //private string _resource;

        //[ObservableProperty]
        //private string _action;

        public int ActionId { get; set; }
        [ObservableProperty]
        private Action _action;
        public int ResourceId { get; set; }
        [ObservableProperty]
        private Resource _resource;

        public Collection<Role> Roles { get; } = new Collection<Role>();
    }

    public static class PermissionActions
    {
        public const string Create = "إنشاء";
        public const string Read = "قراءة";
        public const string Update = "تحديث";
        public const string Delete = "حذف";
    }

    public static class PermissionResources
    {
        public const string Management = "إدارة";
        public const string Users = "المستخدمين";
    }

}

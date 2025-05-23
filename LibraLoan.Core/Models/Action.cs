using CommunityToolkit.Mvvm.ComponentModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Action : ModelBase
    {
        [ObservableProperty]
        private string _name;

        public const string CreateAction = "إنشاء";
        public const string ReadAction = "قراءة";
        public const string UpdateAction = "تحديث";
        public const string DeleteAction = "حذف";
    }
}

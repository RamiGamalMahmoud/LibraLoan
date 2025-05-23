using CommunityToolkit.Mvvm.ComponentModel;

namespace LibraLoan.Core.Models
{
    [ObservableObject]
    public partial class Resource : ModelBase
    {
        [ObservableProperty]
        private string _name;

        public const string ManagementResource = "الإدارة";
        public const string UsersResource = "المستخدمون";
        public const string BooksResource = "الكتب";
        public const string AuthorsResource = "المؤلفون";
        public const string PublishersResource = "دور النشر";
        public const string ClientsResource = "العملاء";
        public const string LoansResource = "الاستعارة";
    }
}

using System.Windows.Controls;

namespace LibraLoan.Features.Management.RolesAndPermissions
{
    internal partial class View : UserControl
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

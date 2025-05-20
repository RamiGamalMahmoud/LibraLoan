using LibraLoan.Core.Abstraction.Features.Management;
using System.Windows.Controls;

namespace LibraLoan.Features.Management.Home
{
    internal partial class View : UserControl, IManagementHomeView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

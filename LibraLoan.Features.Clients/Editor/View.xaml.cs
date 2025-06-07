using System.Windows.Controls;

namespace LibraLoan.Features.Clients.Editor
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

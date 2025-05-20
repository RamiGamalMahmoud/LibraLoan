using System.Windows;

namespace LibraLoan.Features.Clients.Editor
{
    internal partial class View : Window
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

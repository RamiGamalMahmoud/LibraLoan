using System.Windows;

namespace LibraLoan.Features.Authors.Editor
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

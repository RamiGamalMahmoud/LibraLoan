using System.Windows;
using System.Windows.Controls;

namespace LibraLoan.Features.Authors.Editor
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

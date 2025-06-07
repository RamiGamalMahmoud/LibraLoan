using System.Windows;
using System.Windows.Controls;

namespace LibraLoan.Features.Books.Editor
{
    internal partial class View : UserControl
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

            Loaded += View_Loaded;
        }

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                viewModel.LoadDataCommand.ExecuteAsync(false);
            }
        }
    }
}

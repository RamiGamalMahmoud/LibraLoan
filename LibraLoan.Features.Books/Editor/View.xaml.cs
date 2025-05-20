using System.Windows;

namespace LibraLoan.Features.Books.Editor
{
    internal partial class View : Window
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

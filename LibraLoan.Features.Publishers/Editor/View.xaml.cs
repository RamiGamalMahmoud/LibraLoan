using System.Windows;

namespace LibraLoan.Features.Publishers.Editor
{
    internal partial class View : Window
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is ViewModel viewModel)
            {
                await Dispatcher.Invoke(() => viewModel.LoadDataCommand.ExecuteAsync(false));
            }
        }
    }
}

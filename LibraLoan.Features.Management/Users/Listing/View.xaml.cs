using System.Windows;
using System.Windows.Controls;

namespace LibraLoan.Features.Management.Users.Listing
{
    internal partial class View : UserControl
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            Loaded += ViewLoaded;
        }

        private async void ViewLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            if (viewModel is null) return;
            await Dispatcher.InvokeAsync(() => viewModel.LoadDataCommand.ExecuteAsync(false));
        }
    }
}

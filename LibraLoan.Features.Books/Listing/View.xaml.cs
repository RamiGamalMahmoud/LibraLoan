using LibraLoan.Core.Abstraction.Features.Books;
using System.Windows;
using System.Windows.Controls;

namespace LibraLoan.Features.Books.Listing
{
    internal partial class View : UserControl, IBooksListingView
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
                await Dispatcher.InvokeAsync(() =>  viewModel.LoadDataCommand.ExecuteAsync(false));
            }
        }
    }
}

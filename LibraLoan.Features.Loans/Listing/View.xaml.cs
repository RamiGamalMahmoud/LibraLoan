using LibraLoan.Core.Abstraction.Features.Loans;
using System.Windows.Controls;

namespace LibraLoan.Features.Loans.Listing
{
    internal partial class View : UserControl, ILoansListingView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                await Dispatcher.InvokeAsync(() => viewModel.LoadDataCommand.ExecuteAsync(false));
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace LibraLoan.Features.Management.RolesAndPermissions.Roles
{
    internal partial class View : UserControl
    {
        public View()
        {
            InitializeComponent();

            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    viewModel.SearchText = string.Empty;
                    viewModel.LoadDataCommand.ExecuteAsync(false);
                });
            }
        }
    }
}

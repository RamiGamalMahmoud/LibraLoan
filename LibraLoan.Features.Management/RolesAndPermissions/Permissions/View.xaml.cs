using System.Windows.Controls;

namespace LibraLoan.Features.Management.RolesAndPermissions.Permissions
{
    internal partial class View : UserControl
    {
        public View()
        {
            InitializeComponent();

            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel viewModel = (ViewModel)DataContext;
            if(viewModel is null) return;
            await Dispatcher.Invoke(() => viewModel.LoadDataCommand.ExecuteAsync(false));
        }
    }
}

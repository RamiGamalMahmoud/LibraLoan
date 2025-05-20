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

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel?.LoadDataCommand.ExecuteAsync(false);
        }
    }
}

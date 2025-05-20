using LibraLoan.Core.Abstraction.Features.Auth.Login;
using System.Windows;

namespace LibraLoan.Features.Auth.Login
{
    internal partial class View : Window, ILoginView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public new void Show()
        {
            ShowDialog();
        }
    }
}

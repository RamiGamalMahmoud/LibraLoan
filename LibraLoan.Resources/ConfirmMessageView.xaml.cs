using System.Windows;
using System.Windows.Controls;

namespace LibraLoan.Resources
{
    public partial class ConfirmMessageView : Window
    {
        public ConfirmMessageView(string message)
        {
            _message = message;
            DataContext = this;
            InitializeComponent();
        }

        public string Message => string.IsNullOrEmpty(_message) ? "هل تريد بالتأكيد حفظ التعديلات؟" : _message;
        private string _message;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace LibraLoan.Resources
{
    [ObservableObject]
    public partial class DateWindow : Window
    {
        public DateWindow(string message = "", DateTime? compareDate = null)
        {
            InitializeComponent();
            ViewModel = new DateWindowViewModel(message, compareDate);
            DataContext = ViewModel;
        }

        public DateWindowViewModel ViewModel { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

    public partial class DateWindowViewModel : ObservableValidator
    {
        public DateWindowViewModel(string message, DateTime? commpareDate = default)
        {
            Message = message;
            if (commpareDate.HasValue)
            {
                CommpareDate = commpareDate;
            }
            else
            {
                CommpareDate = DateTime.Today;
            }
        }

        public string Message { get; }
        public DateTime? CommpareDate { get; }

        public bool HasMessage => !string.IsNullOrEmpty(Message);


        [ObservableProperty]
        [Required]
        [NotifyDataErrorInfo]
        private DateTime? _selectedDate = DateTime.Today;
    }
}

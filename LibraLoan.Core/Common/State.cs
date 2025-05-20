using CommunityToolkit.Mvvm.ComponentModel;

namespace LibraLoan.Core.Common
{
    public partial class State<T> : ObservableObject
    {
        public State(T value) => Value = value;

        [ObservableProperty]
        private T _value;
    }
}

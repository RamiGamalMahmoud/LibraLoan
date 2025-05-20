namespace LibraLoan.Core.Common
{
    public class SelectableObject<T>
    {
        public SelectableObject(T value)
        {
            Value = value;
        }
        public T Value { get; }


        public bool IsSelected { get; set; }
    }
}

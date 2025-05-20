using LibraLoan.Core.Common;

namespace LibraLoan.Core.Abstraction
{
    public interface IAppStates
    {
        public void SetState<T>(State<T> state);
        public State<T> GetState<T>();
        public void DeleteState<T>();
    }
}

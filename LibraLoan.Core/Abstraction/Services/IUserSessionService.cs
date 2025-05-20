using LibraLoan.Core.Models;

namespace LibraLoan.Core.Abstraction.Services
{
    public interface IUserSessionService
    {
        string UserName { get; }
        int UserId { get; }

        void SetUser(User user);
    }
}

using System.Threading.Tasks;

namespace LibraLoan.Core.Abstraction
{
    public interface IDatabaseService
    {
        Task<bool> CanConnectAsync();
        Task<bool> HasPendingMigrationsAsync();
        Task MigrateAsync();
    }
}

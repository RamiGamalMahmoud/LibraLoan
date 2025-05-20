using LibraLoan.Core.Models;

namespace LibraLoan.Core.DTOs
{
    public record PermissionDto(int Id, Resource Resource, Action Action);
}

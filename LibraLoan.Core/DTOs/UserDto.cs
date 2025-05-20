using LibraLoan.Core.Models;

namespace LibraLoan.Core.DTOs
{
    public record UserDto(int Id, string Username, string Password, Role Role, User CreatedBy, bool IsActive);
}

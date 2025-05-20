using LibraLoan.Core.Models;

namespace LibraLoan.Core.DTOs
{
    public record ClientDto(int Id, string Name, string Email, string Phone, User CreatedBy);
}

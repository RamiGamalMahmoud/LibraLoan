using LibraLoan.Core.Models;

namespace LibraLoan.Core.DTOs
{
    public record AuthorDto(int Id, string Name, User CreatedBy);
}

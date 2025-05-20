using LibraLoan.Core.Models;

namespace LibraLoan.Core.DTOs
{
    public record PublisherDto(int Id, string Name, string Phone, string Email, string Website, string Fax, User CreatedBy);
}

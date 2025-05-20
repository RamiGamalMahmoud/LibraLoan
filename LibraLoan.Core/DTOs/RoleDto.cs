using LibraLoan.Core.Models;
using System.Collections.Generic;

namespace LibraLoan.Core.DTOs
{
    public record RoleDto(int Id, string Name, IEnumerable<Permission> Permissions);
}

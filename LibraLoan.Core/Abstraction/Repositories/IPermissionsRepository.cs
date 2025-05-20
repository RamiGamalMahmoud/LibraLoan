using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraLoan.Core.Abstraction.Repositories
{
    public interface IPermissionsRepository : IRepository<Permission, PermissionDto>
    {
        Task<IEnumerable<Action>> GetAllActions();
        Task<IEnumerable<Resource>> GetAllResources();
    }
}

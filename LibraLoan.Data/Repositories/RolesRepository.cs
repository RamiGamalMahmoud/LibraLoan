using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Data.Repositories
{
    internal class RolesRepository : RepositoryBase<Role>, IRolesRepository
    {
        public RolesRepository(IAppDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<Role> CreateAsync(RoleDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Role role = new Role
                {
                    Name = model.Name,
                    DateCreated = System.DateTime.Now
                };

                IEnumerable<int> permissionIds = model.Permissions.Select(x => x.Id).ToList();

                IEnumerable<Permission> permissions = await dbContext.Permissions.Where(x => permissionIds.Contains(x.Id)).ToListAsync();

                foreach (Permission permission in permissions)
                {
                    role.Permissions.Add(permission);
                }

                try
                {
                    dbContext.Roles.Add(role);
                    await dbContext.SaveChangesAsync();
                    _models.Add(role);
                    return role;
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<Role>> GetAllAsync(bool reload)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Role> roles = await dbContext
                        .Roles
                        .Include(x => x.Permissions).ThenInclude(x => x.Resource)
                        .Include(x => x.Permissions).ThenInclude(x => x.Action)
                        .ToListAsync();
                    SetModels(roles);
                    _isLoaded = true;
                }
                return _models;
            }
        }

        public async Task<bool> UpdateAsync(RoleDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Role role = await dbContext.Roles
                    .Where(x => x.Id == model.Id)
                    .Include(x => x.Permissions)
                    .FirstOrDefaultAsync();

                if (role is null) return false;

                IEnumerable<int> permissionIds = model.Permissions.Select(x => x.Id).ToList();

                IEnumerable<Permission> permissions = await dbContext.Permissions.Where(x => permissionIds.Contains(x.Id)).ToListAsync();

                role.Permissions.Clear();

                foreach (Permission permission in permissions)
                {
                    role.Permissions.Add(permission);
                }

                role.Name = model.Name;

                try
                {
                    dbContext.Roles.Update(role);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }
    }
}

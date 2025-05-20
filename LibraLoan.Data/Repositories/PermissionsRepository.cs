using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Data.Repositories
{
    internal class PermissionsRepository : RepositoryBase<Permission>, IPermissionsRepository
    {
        public PermissionsRepository(IAppDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public override async Task<IEnumerable<Permission>> GetAllAsync(bool reload)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Permission> permissions = await dbContext
                        .Permissions
                        .Include(x => x.Resource)
                        .Include(x => x.Action)
                        .OrderBy(x => x.Resource.Name)
                        .ThenBy(x => x.Action.Name)
                        .ToListAsync();
                    SetModels(permissions);
                    _isLoaded = true;
                }
                return _models;
            }
        }

        public async Task<Permission> CreateAsync(PermissionDto permissionDto)
        {
            using(AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Permission permission = new Permission()
                {
                    ActionId = permissionDto.Action.Id,
                    ResourceId = permissionDto.Resource.Id,
                    DateCreated = System.DateTime.Now
                };

                EntityEntry<Permission> permissionEntry = dbContext.Permissions.Add(permission);

                try
                {
                    await dbContext.SaveChangesAsync();
                    permission.Action = permissionDto.Action;
                    permission.Resource = permissionDto.Resource;
                    _models.Add(permission);
                    return permission;
                }
                catch (DbUpdateException)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Action>> GetAllActions()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isActionsLoaded)
                {
                    IEnumerable<Action> actions = await dbContext
                        .Actions
                        .OrderBy(x => x.Name)
                        .ToListAsync();
                    _actions = new ObservableCollection<Action>(actions);
                    _isActionsLoaded = true;
                }

                return _actions;
            }
        }

        public async Task<IEnumerable<Resource>> GetAllResources()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isResourcesLoaded)
                {
                    IEnumerable<Resource> resources = await dbContext
                        .Resources
                        .OrderBy(x => x.Name)
                        .ToListAsync();
                    _resources = new ObservableCollection<Resource>(resources);
                    _isResourcesLoaded = true;
                }

                return _resources;
            }
        }

        public Task<Action> CreateAction(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resource> CreateResource(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateAsync(PermissionDto model)
        {
            using(AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Permission permission = await dbContext.Permissions.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                permission.ActionId = model.Action.Id;
                permission.ResourceId = model.Resource.Id;
                try
                {
                    dbContext.Permissions.Update(permission);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }

        private bool _isActionsLoaded;
        private bool _isResourcesLoaded;

        private ObservableCollection<Action> _actions;
        private ObservableCollection<Resource> _resources;
    }
}

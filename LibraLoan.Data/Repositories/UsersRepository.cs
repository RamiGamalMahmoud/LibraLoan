using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Data.Repositories
{
    internal class UsersRepository(IAppDbContextFactory dbContextFactory) : RepositoryBase<User>(dbContextFactory), IUsersRepository
    {
        public override async Task<IEnumerable<User>> GetAllAsync(bool reload)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<User> users = await dbContext
                        .Users
                        .Include(x => x.CreatedBy)
                        .Include(x => x.Role)
                        .ToListAsync();
                    SetModels(users);
                    _isLoaded = true;
                }
                return _models;
            }
        }

        public async Task<User> CreateAsync(UserDto userDto)
        {
            User user = new User()
            {
                Username = userDto.Username,
                DateCreated = DateTime.Now,
                IsActive = userDto.IsActive,
                CreatedById = userDto.CreatedBy?.Id,
                RoleId = userDto.Role?.Id
            };

            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                EntityEntry<User> entry = dbContext.Users.Add(user);
                entry.Property<string>("Password").CurrentValue = userDto.Password;

                try
                {
                    await dbContext.SaveChangesAsync();
                    user.CreatedBy = userDto.CreatedBy;
                    user.Role = userDto.Role;
                    _models.Add(user);
                    return user;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<bool> UpdateAsync(UserDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                User user = await dbContext.Users.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                if (user is null) return false;

                user.Username = model.Username;
                user.IsActive = model.IsActive;
                user.RoleId = model.Role?.Id;
                EntityEntry<User> entry = dbContext.Entry(user);
                if (!string.IsNullOrEmpty(model.Password))
                {
                    entry.Property<string>("Password").CurrentValue = model.Password;
                }

                try
                {
                    dbContext.Users.Update(entry.Entity);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}

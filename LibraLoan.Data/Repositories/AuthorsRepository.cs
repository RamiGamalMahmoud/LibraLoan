using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Data.Repositories
{
    internal class AuthorsRepository : RepositoryBase<Author>, IAuthorsRepository
    {
        public AuthorsRepository(IAppDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<Author> CreateAsync(AuthorDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Author author = new Author() { Name = model.Name, CreatedById = model.CreatedBy.Id, DateCreated = System.DateTime.Now };

                try
                {
                    dbContext.Authors.Add(author);
                    await dbContext.SaveChangesAsync();
                    _models.Add(author);
                    return author;
                }
                catch (DbUpdateException)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<Author>> GetAllAsync(bool reload)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Author> authors = await dbContext
                        .Authors
                        .OrderBy(x => x.Name).ThenBy(x => x.CreatedBy.Username)
                        .Include(x => x.CreatedBy)
                        .ToListAsync();
                    SetModels(authors);
                }
                return _models;
            }
        }

        public async Task<bool> UpdateAsync(AuthorDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Author author = dbContext.Authors.Find(model.Id);
                author.Name = model.Name;
                try
                {
                    dbContext.Authors.Update(author);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }
    }
}

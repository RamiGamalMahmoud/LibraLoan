using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraLoan.Data.Repositories
{
    internal class PublishersRepository : RepositoryBase<Publisher>, IPublishersRepository
    {
        public PublishersRepository(IAppDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<Publisher> CreateAsync(PublisherDto model)
        {
            Publisher publisher = new Publisher
            {
                Name = model.Name,
                Phone = model.Phone,
                Fax = model.Fax,
                Email = model.Email,
                DateCreated = DateTime.Now,
                Website = model.Website,
                CreatedById = model.CreatedBy.Id
            };

            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Publishers.Add(publisher);
                try
                {
                    await dbContext.SaveChangesAsync();
                    _models.Add(publisher);
                    publisher.CreatedBy = model.CreatedBy;
                    return publisher;

                }
                catch (DbUpdateException)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<Publisher>> GetAllAsync(bool reload)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Publisher> publishers = await dbContext
                        .Publishers
                        .Include(x => x.CreatedBy)
                        .ToListAsync();
                    SetModels(publishers);
                    _isLoaded = true;
                }
                return _models;
            }
        }

        public async Task<bool> UpdateAsync(PublisherDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Publisher publisher = dbContext.Publishers.Find(model.Id);
                publisher.Name = model.Name;
                publisher.Phone = model.Phone;
                publisher.Fax = model.Fax;
                publisher.Email = model.Email;
                publisher.Website = model.Website;
                try
                {
                    dbContext.Publishers.Update(publisher);
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

using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraLoan.Data.Repositories
{
    internal class ClientsReository(IAppDbContextFactory dbContextFactory) : RepositoryBase<Client>(dbContextFactory), IClientsRepository
    {
        public async Task<Client> CreateAsync(ClientDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Client client = new Client()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    CreatedById = model.CreatedBy.Id,
                    DateCreated = DateTime.Now
                };
                dbContext.Clients.Add(client);
                try
                {
                    await dbContext.SaveChangesAsync();
                    _models.Add(client);
                    client.CreatedBy = model.CreatedBy;
                    return client;
                }
                catch (DbUpdateException)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<Client>> GetAllAsync(bool reload)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Client> clients = await dbContext
                        .Clients
                        .Include(x => x.CreatedBy)
                        .ToListAsync();
                    SetModels(clients);
                }
                return _models;
            }
        }

        public async Task<bool> UpdateAsync(ClientDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Client client = await dbContext.Clients.FindAsync(model.Id);
                client.Name = model.Name;
                client.Email = model.Email;
                client.Phone = model.Phone;
                try
                {
                    dbContext.Clients.Update(client);
                    dbContext.SaveChanges();
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

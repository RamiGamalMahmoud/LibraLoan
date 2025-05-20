using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraLoan.Data.Repositories
{
    internal class LoansRepository(IAppDbContextFactory dbContextFactory) : RepositoryBase<Loan>(dbContextFactory), ILoansRepository
    {
        public async Task CancelReturn(Loan model)
        {
            using(AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Loan loan = await dbContext.Loans.FindAsync(model.Id);
                loan.CancelReturn();
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Loan> CreateAsync(LoanDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                using(IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    Book book = await dbContext.Books.FindAsync(model.Book.Id);
                }
                Loan loan = new Loan()
                {
                    DateCreated = DateTime.Now,
                    ExpectedReturnDate = (DateTime)model.ExpectedReturnDate,
                    LoanDate = model.LoanDate,
                    BookId = model.Book.Id,
                    ClientId = model.Client.Id,
                    CreatedById = model.CreatedBy.Id
                };

                dbContext.Add(loan);
                try
                {
                    await dbContext.SaveChangesAsync();
                    _models.Add(loan);
                    loan.Book = model.Book;
                    loan.Client = model.Client;
                    loan.CreatedBy = model.CreatedBy;
                    return loan;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<Loan>> GetAllAsync(bool reload)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Loan> loans = await dbContext
                        .Loans
                        .Include(x => x.Book)
                        .Include(x => x.Client)
                        .Include(x => x.CreatedBy)
                        .ToListAsync();
                    SetModels(loans);
                }

                return _models;
            }
        }

        public async Task ReturnBook(Loan loan, DateTime returnDate)
        {
            using(AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Loan loanToUpdate = await dbContext.Loans.FindAsync(loan.Id);
                loanToUpdate.Return(returnDate);
                dbContext.SaveChanges();
            }
        }

        public async Task<bool> UpdateAsync(LoanDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Loan loan = await dbContext.Loans.FindAsync(model.Id);
                loan.ExpectedReturnDate = model.ExpectedReturnDate;
                loan.LoanDate = model.LoanDate;
                loan.BookId = model.Book.Id;
                loan.ClientId = model.Client.Id;

                if(model.ActualReturnDate.HasValue)
                {
                    loan.Return(model.ActualReturnDate);
                }

                try
                {
                    dbContext.Loans.Update(loan);
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

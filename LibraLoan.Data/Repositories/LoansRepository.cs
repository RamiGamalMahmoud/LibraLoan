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
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                using (IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    Book book = await dbContext.Books.FindAsync(model.BookId);
                    book.LoanedCopies++;

                    Loan loan = await dbContext.Loans.FindAsync(model.Id);
                    loan.CancelReturn();


                    try
                    {
                        await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task<Loan> CreateAsync(LoanDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                using (IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    Book book = await dbContext.Books.FindAsync(model.Book.Id);
                    book.LoanedCopies++;
                    Loan loan = new Loan()
                    {
                        DateCreated = DateTime.Now,
                        ExpectedReturnDate = (DateTime)model.ExpectedReturnDate,
                        LoanDate = model.LoanDate,
                        BookId = model.Book.Id,
                        ClientId = model.Client.Id,
                        CreatedById = model.CreatedBy.Id
                    };

                    dbContext.Loans.Add(loan);
                    dbContext.Books.Update(book);
                    try
                    {
                        await dbContext.SaveChangesAsync();
                        _models.Add(loan);
                        await transaction.CommitAsync();

                        loan.Book = model.Book;
                        loan.Client = model.Client;
                        loan.CreatedBy = model.CreatedBy;
                        return loan;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        return null;
                    }
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
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                using (IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    Book book = await dbContext.Books.FindAsync(loan.BookId);
                    book.LoanedCopies--;

                    Loan loanToUpdate = await dbContext.Loans.FindAsync(loan.Id);
                    loanToUpdate.Return(returnDate);
                    try
                    {
                        dbContext.SaveChanges();
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
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

                // ActualReturnDate not set
                // ActualReturnDate has value

                if (model.ActualReturnDate.HasValue)
                {
                    Book book = await dbContext.Books.FindAsync(model.Book.Id);
                    book.LoanedCopies++;
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

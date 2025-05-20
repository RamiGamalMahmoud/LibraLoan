using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Data.Repositories
{
    internal class BooksRepository(IAppDbContextFactory dbContextFactory) : RepositoryBase<Book>(dbContextFactory), IBooksRepository
    {
        public async Task<Book> CreateAsync(BookDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Book book = new Book
                {
                    Title = model.Title,
                    Isbn = model.Isbn,
                    PublishDate = model.PublishDate,
                    Version = model.Version,
                    Copies = model.Copies,
                    Photo = model.Photo,
                    PublisherId = model.Publisher.Id,
                    CreatedById = model.CreatedBy.Id,
                    DateCreated = DateTime.Now
                };

                if (model.Authors is not null && model.Authors.Any())
                {
                    IEnumerable<int> authorIds = model.Authors.Select(x => x.Id).ToList();
                    IEnumerable<Author> authors = await dbContext.Authors.Where(x => authorIds.Contains(x.Id)).ToListAsync();
                    foreach (Author author in authors)
                    {
                        book.Authors.Add(author);
                    }
                }

                else
                {
                    return null;
                }

                try
                {
                    dbContext.Books.Add(book);
                    await dbContext.SaveChangesAsync();
                    book.CreatedBy = model.CreatedBy;
                    book.Publisher = model.Publisher;
                    _models.Add(book);
                    return book;
                }
                catch (DbUpdateException)
                {
                    return null;
                }
            }

        }

        public override async Task<IEnumerable<Book>> GetAllAsync(bool reload)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Book> books = await dbContext
                        .Books
                        .OrderBy(x => x.Title)
                        .Include(x => x.Publisher)
                        .Include(x => x.CreatedBy)
                        .Include(x => x.Authors)
                        .OrderBy(x => x.Title)
                        .ToListAsync();
                    SetModels(books);
                    _isLoaded = true;
                }
                return _models;
            }
        }

        public async Task<IEnumerable<Book>> GetAvailableBooks()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<Book> availableBooks = await dbContext
                    .Books
                    .Where(x => x.Copies - x.LoanedCopies > 0)
                    .ToListAsync();

                return availableBooks;
            }
        }

        public async Task<bool> UpdateAsync(BookDto model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Book book = await dbContext.Books.Include(x => x.Authors).Where(x => x.Id == model.Id).FirstAsync();
                book.Title = model.Title;
                book.PublisherId = model.Publisher.Id;
                book.Copies = model.Copies;
                book.Isbn = model.Isbn;
                book.Version = model.Version;
                book.Photo = model.Photo;

                if (model.Authors is not null && model.Authors.Any())
                {
                    book.Authors.Clear();
                    IEnumerable<int> authorIds = model.Authors.Select(x => x.Id).ToList();
                    IEnumerable<Author> authors = await dbContext.Authors.Where(x => authorIds.Contains(x.Id)).ToListAsync();
                    foreach (Author author in authors)
                    {
                        book.Authors.Add(author);
                    }
                }


                try
                {
                    dbContext.Books.Update(book);
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

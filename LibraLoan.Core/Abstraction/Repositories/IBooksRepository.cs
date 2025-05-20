using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraLoan.Core.Abstraction.Repositories
{
    public interface IBooksRepository : IRepository<Book, BookDto>
    {
        Task<IEnumerable<Book>> GetAvailableBooks();
    }
}

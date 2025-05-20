using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using System;
using System.Threading.Tasks;

namespace LibraLoan.Core.Abstraction.Repositories
{
    public interface ILoansRepository : IRepository<Loan, LoanDto>
    {
        Task CancelReturn(Loan loan);
        public Task ReturnBook(Loan loan, DateTime returnDate);
    }
}
 
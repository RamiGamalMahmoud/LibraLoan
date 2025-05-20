using LibraLoan.Core.Models;
using System;

namespace LibraLoan.Core.DTOs
{
    public record LoanDto(int Id, Book Book, Client Client, DateTime LoanDate, DateTime ExpectedReturnDate, DateTime? ActualReturnDate, User CreatedBy);
}

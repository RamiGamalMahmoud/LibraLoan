using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace LibraLoan.Core.Commands
{
    public static class Books
    {
        public record GetAvailableBooksCommand() : IRequest<IEnumerable<Book>>;
    }
}

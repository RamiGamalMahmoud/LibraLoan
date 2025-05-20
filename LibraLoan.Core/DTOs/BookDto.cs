using LibraLoan.Core.Models;
using System;
using System.Collections.Generic;

namespace LibraLoan.Core.DTOs
{
    public record BookDto(int Id,
        string Title,
        string Isbn,
        Publisher Publisher,
        int Version,
        byte[] Photo,
        DateTime PublishDate,
        int Copies,
        User CreatedBy,
        IEnumerable<Author> Authors);
}

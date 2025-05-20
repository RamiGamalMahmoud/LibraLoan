using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraLoan.Data.Configurations
{
    internal class LoanConfiguration : ModelConfiguratioBase<Loan>
    {
        public override void Configure(EntityTypeBuilder<Loan> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.LoanDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("REAL")
                .IsRequired();

            builder.Property(x => x.ActualReturnDate)
                .HasColumnType("DATETIME")
                .IsRequired(false);

            builder.Property(x => x.ExpectedReturnDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(x => x.IsReturned)
                .HasColumnType("bit")
                .IsRequired();

            builder.HasOne(x => x.Book)
                .WithMany(book => book.Loans)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .IsRequired();

        }
    }
}

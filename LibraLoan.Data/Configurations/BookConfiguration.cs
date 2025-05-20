using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraLoan.Data.Configurations
{
    internal class BookConfiguration : ModelConfiguratioBase<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            builder.Property(x => x.Photo)
                .HasColumnType("BLOB")
                .IsRequired(false);

            builder.Property(x => x.Isbn)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder.Property(x => x.DateCreated)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(x => x.Version)
                .HasColumnType("INT")
                .IsRequired(false);

            builder.Property(x => x.Copies)
                .HasColumnType("INT")
                .HasDefaultValue(1)
                .IsRequired();

            builder.Property(x => x.LoanedCopies)
                .HasColumnType("INT")
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(x => x.PublisherId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .IsRequired();

            builder.HasMany(b => b.Authors)
                .WithMany(a => a.Books);

            builder.Ignore(x => x.IsAvailable);
        }
    }
}

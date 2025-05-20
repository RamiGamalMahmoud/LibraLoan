using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraLoan.Data.Configurations
{
    internal class AuthorConfiguration : ModelConfiguratioBase<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)");

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .IsRequired();

            builder.HasMany(x => x.Books).WithMany(x => x.Authors);
        }
    }
}

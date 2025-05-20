using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraLoan.Data.Configurations
{
    internal abstract class ModelConfiguratioBase<TModel> : IEntityTypeConfiguration<TModel> where TModel : ModelBase
    {
        public virtual void Configure(EntityTypeBuilder<TModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasColumnType("datetime").IsRequired();
        }
    }
}

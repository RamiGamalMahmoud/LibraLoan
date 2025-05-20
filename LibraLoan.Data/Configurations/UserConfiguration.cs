using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraLoan.Data.Configurations
{
    internal class UserConfiguration : ModelConfiguratioBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Username)
                .IsRequired();

            builder.Property<string>("Password")
                .IsRequired();

            builder.Property(x => x.IsAdmin).IsRequired().HasDefaultValue(false);

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .IsRequired(false);

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);

            builder.HasIndex(x => x.Username).IsUnique();

            builder.HasData(new
            {
                Id = 1,
                DateCreated = DateTime.Parse("2025-04-01 00:00:00"),
                Username = "admin",
                Password = "$2a$11$Sk2NHXsX3PrWVTrgfmEfa.xw2ViWvadOEFR1leLTq2jSUzu4ak5.2",
                IsActive = true,
                IsAdmin = true
            });
        }
    }
}

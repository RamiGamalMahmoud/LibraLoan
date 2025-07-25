﻿using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraLoan.Data.Configurations
{
    internal class ClientConfiguration : ModelConfiguratioBase<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            builder.Property(x => x.Email)
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .IsRequired();
        }
    }
}

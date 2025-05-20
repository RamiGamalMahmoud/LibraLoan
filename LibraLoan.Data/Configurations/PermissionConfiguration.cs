using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraLoan.Data.Configurations
{
    internal class PermissionConfiguration : ModelConfiguratioBase<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Action)
                .WithMany()
                .HasForeignKey(x => x.ActionId)
                .IsRequired();

            builder.HasOne(x => x.Resource)
                .WithMany()
                .HasForeignKey(x => x.ResourceId)
                .IsRequired();

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Permissions);

            builder.HasIndex(x => new { x.ResourceId, x.ActionId }).IsUnique();
        }
    }

    internal class ActionConfiguration : ModelConfiguratioBase<Action>
    {
        public override void Configure(EntityTypeBuilder<Action> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(new[]
            {
                new { Id = 1, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "إنشاء" },
                new { Id = 2, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "قراءة" },
                new { Id = 3, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "تحديث" },
                new { Id = 4, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "حذف" },
            });
        }
    }

    internal class ResourceConfiguration : ModelConfiguratioBase<Resource>
    {
        public override void Configure(EntityTypeBuilder<Resource> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(new[]
            {
                new { Id = 1, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "المستخدمون" },
                new { Id = 2, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "إدارة" },
                new { Id = 3, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "المؤلفون" },
                new { Id = 4, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "دور النشر" },
                new { Id = 5, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "الكتب" },
                new { Id = 6, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = "الاستعارة" },
            });
        }
    }
}

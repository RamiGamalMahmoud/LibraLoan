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
                new { Id = 1, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Action.CreateAction },
                new { Id = 2, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Action.ReadAction },
                new { Id = 3, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Action.UpdateAction },
                new { Id = 4, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Action.DeleteAction },
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
                new { Id = 1, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Resource.UsersResource },
                new { Id = 2, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Resource.ManagementResource },
                new { Id = 3, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Resource.AuthorsResource },
                new { Id = 4, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Resource.PublishersResource },
                new { Id = 5, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Resource.BooksResource },
                new { Id = 6, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Resource.LoansResource },
                new { Id = 7, DateCreated = System.DateTime.Parse("2025-04-01 00:00:00"), Name = Resource.ClientsResource },
            });
        }
    }
}

using CustomerManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var customer = modelBuilder.Entity<Customer>();

        customer.HasKey(x => x.Id);

        customer.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        customer.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        customer.Property(x => x.Email)
            .HasMaxLength(256)
            .IsRequired();

        customer.HasIndex(x => x.Email).IsUnique();

        customer.Property(x => x.Phone).HasMaxLength(50);

        customer.Property(x => x.CreatedAt).IsRequired();
        customer.Property(x => x.UpdatedAt).IsRequired();
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FamilyAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FamilyAccounting.Data.Context;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Family> Families { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<User>()
            .ToTable("users");

        builder.Entity<User>()
            .Property(user => user.FirstName)
            .HasColumnName("firstname")
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<User>()
            .Property(user => user.LastName)
            .HasColumnName("lastname")
            .HasMaxLength(50)
            .IsRequired(false);
        
        builder.Entity<User>()
            .Property(user => user.UserName)
            .HasColumnName("username")
            .HasMaxLength(50)
            .IsRequired(true);            

        builder.Entity<User>()
            .Property(user => user.PhotoUrl)
            .HasColumnName("photo_url")
            .IsRequired(false);
        
        builder.Entity<User>()
            .HasOne(u => u.Family)
            .WithMany(f => f.Users)
            .HasForeignKey(u => u.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);   
    }
}

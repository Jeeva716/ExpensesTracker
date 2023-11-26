using ExpensesApp.Areas.Identity.Data;
using ExpensesApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;
namespace ExpensesApp.Data;

public class ExpensesAppDBContext : IdentityDbContext<ExpensesAppUser>
{
    public ExpensesAppDBContext(DbContextOptions<ExpensesAppDBContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

    }
}


public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ExpensesAppUser> 
{
    public void Configure(EntityTypeBuilder<ExpensesAppUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);

    }
}


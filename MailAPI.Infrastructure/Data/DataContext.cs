using MailAPI.Domain.Entities;
using MailAPI.Infrastructure.Data.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace MailAPI.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Email> Emails { get; set; }
}
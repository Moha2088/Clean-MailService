using MailAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailAPI.Infrastructure.Data.EntityTypeConfigurations;
public class EmailEntityTypeConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {   
        builder.HasKey(e => e.Id);

        builder.Property(x => x.Subject).HasColumnType("varchar(30)");
        builder.Property(x => x.To).HasColumnType("varchar(30)");



        #region Relations

        builder.HasOne(x => x.User)
            .WithMany(x => x.Emails)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}

using MailAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Infrastructure.Data.EntityTypeConfigurations;
public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnType("varchar(20)");
        builder.Property(x => x.Email).HasColumnType("varchar(30)");
        builder.Property(x => x.Password).HasColumnType("varchar(30)");



        #region Relations

        builder.HasMany(x => x.Emails)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}

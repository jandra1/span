using Culturio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.SetupUser();

            //setting default values, hidden data**
            builder.Property(user => user.IsActive).HasDefaultValueSql("1");
            builder.Property(user => user.TermsAccepted).HasDefaultValueSql("1");
            //builder.Property(user => user.DefaultLanguage).HasDefaultValueSql("English");
            builder.Property(user => user.DateCreated).HasDefaultValueSql("getdate()");

            builder.HasOne(user => user.Company)
                .WithMany(company => company.Users)
                .HasForeignKey(user => user.CompanyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(user => user.CultureObject)
                .WithMany(cultureObject => cultureObject.Users)
                .HasForeignKey(user => user.CultureObjectId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

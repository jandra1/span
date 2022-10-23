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
    public class CultureObjectConfiguration : IEntityTypeConfiguration<CultureObject>
    {
        public void Configure(EntityTypeBuilder<CultureObject> builder)
        {
            builder.ToTable(nameof(CultureObject));

            builder.Property(cultureObject => cultureObject.Name).IsRequired();
            builder.Property(cultureObject => cultureObject.WorkingHours).IsRequired();
            builder.Property(cultureObject => cultureObject.Notes).HasMaxLength(2000);

            builder.HasOne(cultureObject => cultureObject.CultureObjectCompany)
                .WithMany(cultutureObjectCompany => cultutureObjectCompany.CultureObjects)
                .HasForeignKey(cultureObject => cultureObject.CultureObjectCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ResponsiblePerson).WithMany().HasForeignKey(x => x.ResponsiblePersonId).IsRequired(true);

        }

    
    }
}

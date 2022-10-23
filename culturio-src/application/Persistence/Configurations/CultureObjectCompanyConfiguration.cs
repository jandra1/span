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
    public class CultureObjectCompanyConfiguration : IEntityTypeConfiguration<CultureObjectCompany>
    {
        public void Configure(EntityTypeBuilder<CultureObjectCompany> builder)
        {
            builder.ToTable(nameof(CultureObjectCompany));

            builder.SetupEntity();

            builder.HasOne(x => x.CorrespondencePerson).WithMany().HasForeignKey(x => x.CorrespondencePersonId).IsRequired(true);
        }
    }
}

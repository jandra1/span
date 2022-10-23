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
    public class CultureObjectTypeConfiguration : IEntityTypeConfiguration<CultureObjectType>
    {
        public void Configure(EntityTypeBuilder<CultureObjectType> builder)
        {
            builder.ToTable(nameof(CultureObjectType));

            builder.SetupEntity();
        }
    }
}

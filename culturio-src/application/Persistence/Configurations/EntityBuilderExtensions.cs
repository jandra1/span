using Culturio.Domain.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Persistence.Configurations
{
    public static class EntityBuilderExtensions
    {
        public static void SetupUser<T>(this EntityTypeBuilder<T> builder) where T : BaseUserEntity
        {
            builder.Property(x => x.FirstName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.SetupEntity();
        }
        public static void SetupEntity<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.HasKey(x => x.Id);
        }
    }
}

using Culturio.Application.Persistence.Configurations;
using Culturio.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Persistence
{
    public class CulturioDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CultureObject> CultureObjects { get; set; }
        public DbSet<CultureObjectCompany> CultureObjectCompanies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CultureObjectType> CultureObjectTypes { get; set; }
        public DbSet<Sex> Sex { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public CulturioDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}

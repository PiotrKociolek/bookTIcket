using Microsoft.EntityFrameworkCore;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Shared.Application.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

namespace Template.Modules.Core.Application.EF
{
    public class TemplateContext : IdentityDbContext<User>, IDataProtectionKeyContext
    {
        private readonly SqlSettings _sqlSettings;

        public TemplateContext()
        {
        }

        public TemplateContext(DbContextOptions<TemplateContext> options, SqlSettings sqlSettings)
            : base(options)
        {
            _sqlSettings = sqlSettings;
        }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_sqlSettings.GetConnectionString(), x => x.MigrationsAssembly("Template.Modules.Core.Infrastructure"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TemplateContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
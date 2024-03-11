using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Modules.Core.Core.Domain;

namespace Template.Modules.Core.Application.EF.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired(false);

            builder.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();      
            
            builder.Property(e => e.ChangePassword)
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
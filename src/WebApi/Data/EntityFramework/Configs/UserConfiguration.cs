using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Domains;

namespace WebApi.Data.EntityFramework.Configs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(64);
            
            builder.HasMany(x => x.Followers)
                .WithMany(x => x.Subscriptions)
                .UsingEntity(j => j.ToTable("Subscriptions"));
        }
    }
}
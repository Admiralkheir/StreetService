using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreetService.Domain.Entities;

namespace StreetService.Data.Configurations
{
    public class StreetConfiguration : IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("now()");
        }
    }
}

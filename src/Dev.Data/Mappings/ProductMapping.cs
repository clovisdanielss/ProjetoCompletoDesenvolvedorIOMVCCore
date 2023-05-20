using Dev.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dev.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasColumnType("varchar(100)");

            builder.Property(x => x.Description)
                   .IsRequired()
                   .HasColumnType("varchar(1000)");

            builder.Property(x => x.Image)
                   .IsRequired()
                   .HasColumnType("varchar(200)");
        }
    }
}

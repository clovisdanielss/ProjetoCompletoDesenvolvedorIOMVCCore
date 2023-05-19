using Dev.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dev.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.Street)
                   .IsRequired()
                   .HasColumnType("varchar(100)");

            builder.Property(x => x.Number)
                   .IsRequired()
                   .HasColumnType("varchar(10)");

            builder.Property(x => x.ZipCode)
                   .IsRequired()
                   .HasColumnType("varchar(8)");

            builder.Property(x => x.Neighboor)
                   .IsRequired()
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.City)
                   .IsRequired()
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.State)
                   .IsRequired()
                   .HasColumnType("varchar(2)");
        }
    }
}

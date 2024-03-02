using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        //esta clase nos permitira configurar la entidad Product
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //por ejemplo
            //builder.Property(p=> p.Id).IsRequired();
            builder.Property(p=> p.Name).HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            
        }
    }

}
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
            builder.Property(p=> p.Name).HasMaxLength(100).IsRequired(false);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            
            //indicamos que productBrand tiene una relacion 1-muchos
            //e indicamos que la llave foranea es product brandId
            builder.HasOne(b => b.ProductBrand).WithMany()
            .HasForeignKey(p=> p.ProductBrandId);

            builder.HasOne(t => t.ProductType).WithMany()
            .HasForeignKey(p => p.ProductTypeId);
        }
    }
}
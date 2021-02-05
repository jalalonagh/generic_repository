
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Shop
{
    /// <summary>
    /// جدول اطلاعات یک محصول
    /// </summary>
    public class ProductPhoto : BaseEntity<int>
    {
        public int productId { get; set; }
        public virtual Product product { get; set; }
        public string altText { get; set; }
        public string photo { get; set; }
    }

    public class ProductPhotoConfiguration : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> builder)
        {
            builder.ToTable(nameof(ProductPhoto), SchemaEnum.Shop.ToString());

            builder.Property(p => p.productId).IsRequired();
            builder.Property(p => p.altText).IsRequired();

            builder.HasOne(o => o.product).WithMany(w => w.photos).HasForeignKey(f => f.productId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

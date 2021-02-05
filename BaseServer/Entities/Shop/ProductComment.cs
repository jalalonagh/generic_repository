
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Shop
{
    /// <summary>
    /// جدول اطلاعات یک محصول
    /// </summary>
    public class ProductComment : BaseEntity<int>
    {
        public int productId { get; set; }
        public virtual Product product { get; set; }
        public string comment { get; set; }
        public int buyRate { get; set; }
        public int productRate { get; set; }
        public int featureRate { get; set; }
        public int technologyRate { get; set; }
    }

    public class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.ToTable(nameof(ProductComment), SchemaEnum.Shop.ToString());

            builder.Property(p => p.productId).IsRequired();
            builder.Property(p => p.comment).IsRequired();
            builder.Property(p => p.buyRate).IsRequired();
            builder.Property(p => p.productRate).IsRequired();
            builder.Property(p => p.featureRate).IsRequired();
            builder.Property(p => p.technologyRate).IsRequired();

            builder.HasOne(o => o.product).WithMany(w => w.comments).HasForeignKey(f => f.productId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

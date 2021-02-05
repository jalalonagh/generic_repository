
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Shop
{
    /// <summary>
    /// جدول اطلاعات یک محصول
    /// </summary>
    public class ProductFeature : BaseEntity<int>
    {
        /// <summary>
        /// شناسه محصول
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// مدل محصول
        /// </summary>
        public virtual Product product { get; set; }
        /// <summary>
        /// پارامتر
        /// </summary>
        public string parameter { get; set; }
        /// <summary>
        /// مقدار مشخصه
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// دسته بندی
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// نوع مقدار ویژگی
        /// </summary>
        public string valueType { get; set; }
    }

    public class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.ToTable(nameof(ProductFeature), SchemaEnum.Shop.ToString());

            builder.Property(p => p.productId).IsRequired();
            builder.Property(p => p.parameter).IsRequired();
            builder.Property(p => p.value).IsRequired();

            builder.HasOne(o => o.product).WithMany(w => w.features).HasForeignKey(f => f.productId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }

}

using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Shop
{
    /// <summary>
    /// جدول محصول
    /// </summary>
    public class Product: BaseEntity<int>
    {
        /// <summary>
        /// نام محصول
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// نام نمایشی
        /// </summary>
        public string displayName { get; set; }
        /// <summary>
        /// آدرس تصویر آیکنی محصول
        /// </summary>
        public string pictureIcon { get; set; }
        /// <summary>
        /// آدرس تصویر کوچک محصول
        /// </summary>
        public string pictureSmall { get; set; }
        /// <summary>
        /// آدرس تصویر بزرگ محصول
        /// </summary>
        public string pictureLarge { get; set; }
        /// <summary>
        /// آدرس تصویر اصلی محصول
        /// </summary>
        public string picture { get; set; }
        /// <summary>
        /// بارکد
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// کیو آر کد
        /// </summary>
        public string qrCode { get; set; }
        /// <summary>
        /// شرح محصول
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// فهرست ویژگی ها
        /// </summary>
        public virtual IEnumerable<ProductFeature> features { get; set; }
        public virtual IEnumerable<ProductComment> comments { get; set; }
        public virtual IEnumerable<ProductPhoto> photos { get; set; }
        /// <summary>
        /// فهرست محصولات فروشگاه
        /// </summary>
        public virtual IEnumerable<ShopProduct> shopProducts { get; set; }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product), SchemaEnum.Shop.ToString());

            builder.Property(p => p.name).IsRequired().IsUnicode();
            builder.Property(p => p.displayName).IsRequired().IsUnicode();
            builder.Property(p => p.barcode).IsUnicode();
            builder.Property(p => p.qrCode).IsUnicode();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

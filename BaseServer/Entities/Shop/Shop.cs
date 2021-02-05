using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Shop
{
    /// <summary>
    /// جدول فروشگاه
    /// </summary>
    public class Shop: BaseEntity<int>
    {
        /// <summary>
        /// نام فروشگاه
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// شرح فروشگاه
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// آدرس تصویر لوگو
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// شماره تماس
        /// </summary>
        public string contact { get; set; }
        /// <summary>
        /// آدرس پستی
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// مختصات x
        /// </summary>
        public double? lat { get; set; }
        /// <summary>
        /// مختصات y
        /// </summary>
        public double? lng { get; set; }

        /// <summary>
        /// فهرست محصولات داخل فروشگاه
        /// </summary>
        public virtual IEnumerable<ShopProduct> shopProducts { get; set; }
    }

    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable(nameof(Shop), SchemaEnum.Shop.ToString());
            builder.Property(p => p.name).IsRequired().IsUnicode();
            builder.Property(p => p.contact).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

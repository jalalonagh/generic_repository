using Common;
using Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Shop
{
    /// <summary>
    /// جدول محصولات فروشگاه
    /// </summary>
    public class ShopProductActivity: BaseEntity<int>
    {

        public int shopProductId { get; set; }
        /// <summary>
        /// مدل محصول
        /// </summary>
        [JsonIgnore]
        public virtual ShopProduct shopProduct { get; set; }
        public string activityType { get; set; }
        public string value { get; set; }
        public int? userId { get; set; }
    }

    public class ShopProductActivityConfiguration : IEntityTypeConfiguration<ShopProductActivity>
    {
        public void Configure(EntityTypeBuilder<ShopProductActivity> builder)
        {
            builder.ToTable(nameof(ShopProductActivity), SchemaEnum.Shop.ToString());
            builder.Property(p => p.shopProductId).IsRequired();
            builder.Property(p => p.activityType).IsRequired();

            builder.HasOne(z => z.shopProduct).WithMany(z => z.Activities).HasForeignKey(z => z.shopProductId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

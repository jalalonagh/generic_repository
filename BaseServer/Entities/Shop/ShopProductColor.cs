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
    public class ShopProductColor: BaseEntity<int>
    {

        public int shopProductId { get; set; }
        [JsonIgnore]
        public virtual ShopProduct shopProduct { get; set; }
        public string color { get; set; }
        public string colorName { get; set; }
        public int additionalPrice { get; set; }
    }

    public class ShopProductColorConfiguration : IEntityTypeConfiguration<ShopProductColor>
    {
        public void Configure(EntityTypeBuilder<ShopProductColor> builder)
        {
            builder.ToTable(nameof(ShopProductColor), SchemaEnum.Shop.ToString());
            builder.Property(p => p.shopProductId).IsRequired();
            builder.Property(p => p.color).IsRequired();
            builder.Property(p => p.colorName).IsRequired();
            builder.Property(p => p.additionalPrice).HasDefaultValue(0);

            builder.HasOne(z => z.shopProduct).WithMany(z => z.Colors).HasForeignKey(z => z.shopProductId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

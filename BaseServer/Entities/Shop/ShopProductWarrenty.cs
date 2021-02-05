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
    public class ShopProductWarrenty: BaseEntity<int>
    {

        public int shopProductId { get; set; }
        [JsonIgnore]
        public virtual ShopProduct shopProduct { get; set; }
        public int month { get; set; }
        public string warrentyName { get; set; }
        public int additionalPrice { get; set; }
    }

    public class ShopProductWarrentyConfiguration : IEntityTypeConfiguration<ShopProductWarrenty>
    {
        public void Configure(EntityTypeBuilder<ShopProductWarrenty> builder)
        {
            builder.ToTable(nameof(ShopProductWarrenty), SchemaEnum.Shop.ToString());
            builder.Property(p => p.shopProductId).IsRequired();
            builder.Property(p => p.month).IsRequired();
            builder.Property(p => p.warrentyName).IsRequired();
            builder.Property(p => p.additionalPrice).HasDefaultValue(0);

            builder.HasOne(z => z.shopProduct).WithMany(z => z.Warrenties).HasForeignKey(z => z.shopProductId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

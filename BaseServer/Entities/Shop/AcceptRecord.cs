using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Shop
{
    /// <summary>
    /// جدول تائید موارد
    /// </summary>
    public class AcceptRecord: BaseEntity<int>
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// شناسه آیتم
        /// </summary>
        public int itemId { get; set; }
        /// <summary>
        /// نام جدول
        /// </summary>
        public string itemType { get; set; } // like this "Product" OR "Shop"
        /// <summary>
        /// تائید
        /// </summary>
        public bool acceptItem { get; set; }
    }

    public class AcceptRecordConfiguration : IEntityTypeConfiguration<AcceptRecord>
    {
        public void Configure(EntityTypeBuilder<AcceptRecord> builder)
        {
            builder.ToTable(nameof(AcceptRecord), SchemaEnum.Shop.ToString());

            builder.Property(p => p.userId).IsRequired();
            builder.Property(p => p.itemId).IsRequired();
            builder.Property(p => p.itemType).IsRequired();
            builder.Property(p => p.acceptItem).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Shop
{
    /// <summary>
    /// جدول لایک ها
    /// </summary>
    public class Like: BaseEntity<int>
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int? userId { get; set; }
        /// <summary>
        /// آدری آی پی
        /// </summary>
        public string ipAddress { get; set; }
        /// <summary>
        /// موبایل ؟
        /// </summary>
        public string isMobile { get; set; }
        /// <summary>
        /// سیستم عامل
        /// </summary>
        public string operation { get; set; }
        /// <summary>
        /// شناسه آیتم
        /// </summary>
        public int itemId { get; set; }
        /// <summary>
        /// نوع آیتم
        /// </summary>
        public string itemType { get; set; }
        /// <summary>
        /// لایک ؟
        /// </summary>
        public bool liked { get; set; }
    }

    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable(nameof(Like), SchemaEnum.Shop.ToString());

            builder.Property(p => p.userId).IsRequired();
            builder.Property(p => p.itemId).IsRequired();
            builder.Property(p => p.itemType).IsRequired();
            builder.Property(p => p.liked).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

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
    public class Warrenty: BaseEntity<int>
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int? userId { get; set; }
        /// <summary>
        /// آدری آی پی
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// موبایل ؟
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// سیستم عامل
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// شناسه آیتم
        /// </summary>
        public int phone { get; set; }
    }

    public class WarrentyConfiguration : IEntityTypeConfiguration<Warrenty>
    {
        public void Configure(EntityTypeBuilder<Warrenty> builder)
        {
            builder.ToTable(nameof(Warrenty), SchemaEnum.Shop.ToString());

            builder.Property(p => p.name).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

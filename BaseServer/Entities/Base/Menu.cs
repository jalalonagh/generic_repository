
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Base
{
    /// <summary>
    /// جدول منو های سایت
    /// </summary>
    public class Menu : BaseEntity<int>
    {
        /// <summary>
        /// نام منویی که استفاده می شود
        /// </summary>
        public string menu { get; set; }
        /// <summary>
        /// نام یک لینک
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// عنوان نمایشی
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// آدرس لینک
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// ریشه
        /// </summary>
        public int parent { get; set; }
        /// <summary>
        /// نام کلاس های استایل 
        /// </summary>
        public string customClassStyleNames { get; set; }
    }

    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable(nameof(Menu), SchemaEnum.Base.ToString());

            builder.Property(p => p.name).IsRequired().IsUnicode();
            builder.Property(p => p.title).IsRequired();
            builder.Property(p => p.url).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }

}

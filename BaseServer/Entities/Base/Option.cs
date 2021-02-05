
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Base
{
    /// <summary>
    /// جدول تنظیمات
    /// </summary>
    public class Option : BaseEntity<int>
    {
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// نوع تنظیم
        /// </summary>
        public string type { get; set; } = "general";
        /// <summary>
        /// مقدار
        /// </summary>
        public string value { get; set; }
    }

    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable(nameof(Option), SchemaEnum.Base.ToString());

            builder.Property(p => p.title).IsRequired();
            builder.Property(p => p.name).IsRequired().IsUnicode();
            builder.Property(p => p.type).IsRequired();
            builder.Property(p => p.value).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

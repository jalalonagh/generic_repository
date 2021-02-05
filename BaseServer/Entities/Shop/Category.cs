using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Shop
{
    /// <summary>
    /// جدول دسته بندی
    /// </summary>
    public class Category: BaseEntity<int>
    {
        /// <summary>
        /// عنوان دسته بندی
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// شرح دسته بندی
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// آدرس آیکن
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// شناسه والد
        /// </summary>
        public int parentId { get; set; }
        /// <summary>
        /// نوع دسته بندی
        /// </summary>
        public string categoryMode { get; set; }
        public string tree { get; set; }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category), SchemaEnum.Shop.ToString());
            builder.Property(p => p.title).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

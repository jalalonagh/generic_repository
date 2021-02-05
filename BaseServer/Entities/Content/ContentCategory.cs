using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Content
{
    public class ContentCategory : BaseEntity<int>
    {
        public string categoryName { get; set; }
        public string categoryTitle { get; set; }
        public string categoryDescription { get; set; }
        public string language { get; set; }
        public int categoryParent { get; set; }
        public string defaultKeywords { get; set; }
        public string defaultTags { get; set; }
        public string categoryImage { get; set; }
        public string color { get; set; }
        public string backgroundColor { get; set; }
    }

    public class ContentCategoryConfiguration : IEntityTypeConfiguration<ContentCategory>
    {
        public void Configure(EntityTypeBuilder<ContentCategory> builder)
        {
            builder.ToTable(nameof(ContentCategory), SchemaEnum.CMS.ToString());

            builder.Property(p => p.categoryName).IsRequired().IsUnicode();
            builder.Property(p => p.categoryTitle).IsRequired();
            builder.Property(p => p.language).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

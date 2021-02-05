using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Content
{
    public class Content : BaseEntity<int>
    {
        public string contentName { get; set; }
        public string contentTitle { get; set; }
        public string contentSummary { get; set; }
        public string contentText { get; set; }
        public string language { get; set; }
        public int? contentParent { get; set; }
        public string defaultKeywords { get; set; }
        public string defaultTags { get; set; }
        public string contentImage { get; set; }
    }

    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.ToTable(nameof(Content), SchemaEnum.CMS.ToString());

            builder.Property(p => p.contentName).IsRequired().IsUnicode();
            builder.Property(p => p.contentTitle).IsRequired();
            builder.Property(p => p.contentSummary).IsRequired();
            builder.Property(p => p.contentText).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

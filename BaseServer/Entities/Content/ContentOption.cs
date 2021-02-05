using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Content
{
    public class ContentOption : BaseEntity<int>
    {
        public string optionType { get; set; }
        public string optionName { get; set; }
        public string optionValue { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string attachedFile { get; set; }
    }

    public class ContentOptionConfiguration : IEntityTypeConfiguration<ContentOption>
    {
        public void Configure(EntityTypeBuilder<ContentOption> builder)
        {
            builder.ToTable(nameof(ContentOption), SchemaEnum.CMS.ToString());

            builder.Property(p => p.optionType).IsRequired();
            builder.Property(p => p.optionName).IsRequired().IsUnicode();
            builder.Property(p => p.optionValue).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

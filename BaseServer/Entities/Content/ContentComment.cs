using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Content
{
    public class ContentComment: BaseEntity<int>
    {
        public int postId { get; set; }
        //[JsonIgnore]
        //public virtual PostContent post { get; set; }
        public string commentAuthor { get; set; }
        public string authorEmail { get; set; }
        public string authorWebsite { get; set; }
        public string authorIP { get; set; }
        public string commentText { get; set; }
        public int commentParent { get; set; }
    }

    public class ContentCommentConfiguration : IEntityTypeConfiguration<ContentComment>
    {
        public void Configure(EntityTypeBuilder<ContentComment> builder)
        {
            builder.ToTable(nameof(ContentComment), SchemaEnum.CMS.ToString());

            builder.Property(p => p.postId).IsRequired();
            builder.Property(p => p.commentText).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

using Common;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Content
{
    public class FileContent: BaseEntity<int>
    {
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string fileType { get; set; }
        /// <summary>
        /// مگابایت
        /// </summary>
        public double size { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string path { get; set; }
    }

    public class FileContentConfiguration : IEntityTypeConfiguration<FileContent>
    {
        public void Configure(EntityTypeBuilder<FileContent> builder)
        {
            builder.ToTable(nameof(FileContent), SchemaEnum.CMS.ToString());

            builder.Property(p => p.name).IsRequired();
            builder.Property(p => p.path).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

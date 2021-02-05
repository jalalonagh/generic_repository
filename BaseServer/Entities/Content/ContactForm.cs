using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Content
{
    public class ContactForm: BaseEntity<int>
    {
        public string fullName { get; set; }
        public string phone { get; set; }
        public string company { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }

    public class ContactFormConfiguration : IEntityTypeConfiguration<ContactForm>
    {
        public void Configure(EntityTypeBuilder<ContactForm> builder)
        {
            builder.ToTable(nameof(ContactForm), SchemaEnum.CMS.ToString());

            builder.Property(p => p.fullName).IsRequired();
            builder.Property(p => p.phone).IsRequired();
            builder.Property(p => p.title).IsRequired();
            builder.Property(p => p.description).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

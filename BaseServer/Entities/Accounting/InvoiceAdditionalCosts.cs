using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Accounting
{
    /// <summary>
    /// جدول هزینه های اضافی فاکتور
    /// </summary>
    public class InvoiceAdditionalCost: BaseEntity<int>
    {
        /// <summary>
        /// شناسه فاکتور
        /// </summary>
        public int invoiceId { get; set; }
        /// <summary>
        /// مدل فاکتور
        /// </summary>
        public virtual Invoice invoice { get; set; }
        /// <summary>
        /// شناسه هزینه اضافی
        /// </summary>
        public int additionalCostId { get; set; }
        /// <summary>
        /// مدل هزینه اضافی
        /// </summary>
        public AdditionalCost additionalCost { get; set; }
        /// <summary>
        /// مبلغ هزینه
        /// </summary>
        public int cost { get; set; }
    }

    public class InvoiceAdditionalCostConfiguration : IEntityTypeConfiguration<InvoiceAdditionalCost>
    {
        public void Configure(EntityTypeBuilder<InvoiceAdditionalCost> builder)
        {
            builder.ToTable(nameof(InvoiceAdditionalCost), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.invoiceId).IsRequired();
            builder.Property(p => p.additionalCostId).IsRequired();
            builder.Property(p => p.cost).IsRequired().IsUnicode();

            builder.HasOne(o => o.invoice).WithMany(m => m.additionalCosts).HasForeignKey(f => f.invoiceId);
            builder.HasOne(o => o.additionalCost).WithMany(m => m.additionalCosts).HasForeignKey(f => f.additionalCostId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

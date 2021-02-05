using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Accounting
{
    /// <summary>
    /// جدول رسید پرداخت
    /// </summary>
    public class PaymentRecipt: BaseEntity<int>
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
        /// جمع پرداختی
        /// </summary>
        public int totalPayment { get; set; }
        /// <summary>
        /// شناسه بانک
        /// </summary>
        public int bankId { get; set; }
        /// <summary>
        /// مدل بانک
        /// </summary>
        public virtual BankAccount bank { get; set; }
        /// <summary>
        /// کد امنیتی
        /// </summary>
        public string authority { get; set; }
        /// <summary>
        /// کد رهگیری
        /// </summary>
        public string refId { get; set; }
    }

    public class PaymentReciptConfiguration : IEntityTypeConfiguration<PaymentRecipt>
    {
        public void Configure(EntityTypeBuilder<PaymentRecipt> builder)
        {
            builder.ToTable(nameof(PaymentRecipt), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.invoiceId).IsRequired();
            builder.Property(p => p.totalPayment).IsRequired();
            builder.Property(p => p.bankId).IsRequired();

            builder.HasOne(o => o.invoice).WithMany(m => m.recipts).HasForeignKey(f => f.invoiceId);
            builder.HasOne(o => o.bank).WithMany(m => m.recipts).HasForeignKey(f => f.bankId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

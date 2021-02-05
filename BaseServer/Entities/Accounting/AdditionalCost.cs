using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Accounting
{
    /// <summary>
    /// جدول هزینه های اضافی
    /// </summary>
    public class AdditionalCost: BaseEntity<int>
    {
        /// <summary>
        /// عنوان هزینه
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// نام هزینه - منحصر به فرد
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// شرح
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// مبلغ هزینه به زیال
        /// </summary>
        public int cost { get; set; }
        /// <summary>
        /// مبلغ هزینه ارزی
        /// </summary>
        public int nationalCurrencyCost { get; set; }
        /// <summary>
        /// شناسه ارز
        /// </summary>
        public int currencyId { get; set; }
        /// <summary>
        /// مدل ارز
        /// </summary>
        public NationalCurrency currency { get; set; }

        /// <summary>
        /// هزینه های اضافی
        /// </summary>
        public virtual IEnumerable<InvoiceAdditionalCost> additionalCosts { get; set; }
        public virtual IEnumerable<CartAdditionalCost> cartAdditionalCosts { get; set; }
    }

    public class AdditionalCostConfiguration : IEntityTypeConfiguration<AdditionalCost>
    {
        public void Configure(EntityTypeBuilder<AdditionalCost> builder)
        {
            builder.ToTable(nameof(AdditionalCost), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.title).IsRequired();
            builder.Property(p => p.name).IsRequired().IsUnicode();
            builder.Property(p => p.cost).IsRequired();

            builder.HasOne(o => o.currency).WithMany(m => m.additionalCosts).HasForeignKey(f => f.currencyId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

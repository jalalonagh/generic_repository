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
    ///  جدول نرخ ارز
    /// </summary>
    public class ExchageRate: BaseEntity<int>
    {
        /// <summary>
        /// شناسه ارز
        /// </summary>
        public int nationalCurrencyId { get; set; }
        /// <summary>
        /// مدل ارز
        /// </summary>
        [JsonIgnore]
        public virtual NationalCurrency nationalCurrency { get; set; }
        /// <summary>
        /// هزینه در ازای یک واحد
        /// </summary>
        public int cost { get; set; }
    }

    public class ExchageRateConfiguration : IEntityTypeConfiguration<ExchageRate>
    {
        public void Configure(EntityTypeBuilder<ExchageRate> builder)
        {
            builder.ToTable(nameof(ExchageRate), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.nationalCurrencyId).IsRequired();
            builder.Property(p => p.cost).IsRequired();

            builder.HasOne(o => o.nationalCurrency).WithMany(m => m.rates).HasForeignKey(f => f.nationalCurrencyId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

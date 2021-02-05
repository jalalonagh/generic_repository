using Common;
using Entities.Shop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Accounting
{
    /// <summary>
    /// جدول ارز های بین المللی
    /// </summary>
    public class NationalCurrency: BaseEntity<int>
    {
        /// <summary>
        /// عنوان ارز
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// نام ارز
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// سمبل بین المللی
        /// </summary>
        public string symbol { get; set; }
        /// <summary>
        /// سمبل یا نام ایرانی
        /// </summary>
        public string persianSymbol { get; set; }
        /// <summary>
        /// آدرس آیکن
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// فهست تغییرات نرخ ارز
        /// </summary>
        public virtual IEnumerable<ExchageRate> rates { get; set; }
        /// <summary>
        /// هزینه های اضافی
        /// </summary>
        public virtual IEnumerable<AdditionalCost> additionalCosts { get; set; }
        /// <summary>
        /// محصولات دارای ارز مورد نظر
        /// </summary>
        public virtual IEnumerable<ShopProduct> shopProducts { get; set; }
    }

    public class NationalCurrencyConfiguration : IEntityTypeConfiguration<NationalCurrency>
    {
        public void Configure(EntityTypeBuilder<NationalCurrency> builder)
        {
            builder.ToTable(nameof(NationalCurrency), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.title).IsRequired();
            builder.Property(p => p.name).IsRequired().IsUnicode();
            builder.Property(p => p.symbol).IsRequired().IsUnicode();
            builder.Property(p => p.persianSymbol).IsUnicode().IsRequired();

            //builder.HasOne(o => o.user).WithMany(m => m.banks).HasForeignKey(f => f.userId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

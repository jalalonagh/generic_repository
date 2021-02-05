
using Common;
using Entities.Shop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;

namespace Entities.Accounting
{
    /// <summary>
    /// جدول کالای فاکتور
    /// </summary>
    public class InvoiceProduct : BaseEntity<int>
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
        /// شناسه کالای انبار
        /// </summary>
        public int shopProductId { get; set; }
        /// <summary>
        /// مدل کالای انبار
        /// </summary>
        public virtual ShopProduct shopProduct { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public int qty { get; set; }
        /// <summary>
        /// قیمت واحد
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// جمع کل
        /// </summary>
        public int totalPrice { get; set; }
        public int? colorId { get; set; }
        public int colorPrice { get; set; }
        public int? warrentyId { get; set; }
        public int warrentyPrice { get; set; }

    }

    public class InvoiceProductCostConfiguration : IEntityTypeConfiguration<InvoiceProduct>
    {
        public void Configure(EntityTypeBuilder<InvoiceProduct> builder)
        {
            builder.ToTable(nameof(InvoiceProduct), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.invoiceId).IsRequired();
            builder.Property(p => p.shopProductId).IsRequired();
            builder.Property(p => p.qty).IsRequired();
            builder.Property(p => p.price).IsRequired();
            builder.Property(p => p.totalPrice).IsRequired();

            builder.HasOne(o => o.invoice).WithMany(m => m.invoiceProducts).HasForeignKey(f => f.invoiceId);
            builder.HasOne(o => o.shopProduct).WithMany(m => m.invoiceProducts).HasForeignKey(f => f.shopProductId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

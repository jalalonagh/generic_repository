using Common;
using Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Shop
{
    /// <summary>
    /// جدول محصولات فروشگاه
    /// </summary>
    public class ShopProduct: BaseEntity<int>
    {
        /// <summary>
        /// بارکد
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// کیو آر کد
        /// </summary>
        public string qrCode { get; set; }
        /// <summary>
        /// شناسه فروشگاه
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// مدل فروشگاه
        /// </summary>
        [JsonIgnore]
        public virtual Shop shop { get; set; }
        /// <summary>
        /// شناسه محصول
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// مدل محصول
        /// </summary>
        [JsonIgnore]
        public virtual Product product { get; set; }
        /// <summary>
        /// تعداد در انبار
        /// </summary>
        public int inventory { get; set; }
        /// <summary>
        /// مبلغ فروش
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// شناسه ارز فروش
        /// </summary>
        public int? nationalCurrencyId { get; set; }
        /// <summary>
        /// مدل ارز مورد استفاده
        /// </summary>
        [JsonIgnore]
        public virtual NationalCurrency nationalCurrency { get; set; }
        /// <summary>
        /// مبلغ ارز مورد استفاده
        /// </summary>
        public int? currencyPrice { get; set; }
        /// <summary>
        /// مبلغ خرید
        /// </summary>
        public int buyPrice { get; set; }
        /// <summary>
        /// مبلغ ارزی خرید
        /// </summary>
        public int? currencyBuyPrice { get; set; }
        /// <summary>
        /// مبلغ تخفیف => 0.05 معادل 5 درصد
        /// </summary>
        public double? off_price { get; set; }
        /// <summary>
        /// زمان انقضاء تخفیف
        /// </summary>
        public DateTime? off_expire { get; set; }
        /// <summary>
        /// تماس بگیرید
        /// </summary>
        public bool? call_us { get; set; }
        /// <summary>
        /// پیشنهاد ویژه
        /// </summary>
        public bool specialOffer { get; set; }
        /// <summary>
        /// جنس دست دوم
        /// </summary>
        public bool stock { get; set; }
        public string warrenty { get; set; }
        /// <summary>
        /// فهرست کالا های یک فاکتور
        /// </summary>
        public IEnumerable<InvoiceProduct> invoiceProducts { get; set; }
        public IEnumerable<CartProduct> cartProducts { get; set; }
        public IEnumerable<ShopProductActivity> Activities { get; set; }
        public IEnumerable<ShopProductColor> Colors { get; set; }
        public IEnumerable<ShopProductWarrenty> Warrenties { get; set; }
    }

    public class ShopProductConfiguration : IEntityTypeConfiguration<ShopProduct>
    {
        public void Configure(EntityTypeBuilder<ShopProduct> builder)
        {
            builder.ToTable(nameof(ShopProduct), SchemaEnum.Shop.ToString());
            builder.Property(p => p.productId).IsRequired();
            builder.Property(p => p.shopId).IsRequired();
            builder.Property(p => p.buyPrice).IsRequired();

            builder.HasOne(z => z.product).WithMany(z => z.shopProducts).HasForeignKey(z => z.productId);
            builder.HasOne(z => z.shop).WithMany(z => z.shopProducts).HasForeignKey(z => z.shopId);
            builder.HasOne(z => z.nationalCurrency).WithMany(z => z.shopProducts).HasForeignKey(z => z.nationalCurrencyId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

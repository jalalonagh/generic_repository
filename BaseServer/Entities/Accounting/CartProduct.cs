
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
    public class CartProduct : BaseEntity<int>
    {
        /// <summary>
        /// شناسه فاکتور
        /// </summary>
        public int cartId { get; set; }
        /// <summary>
        /// مدل فاکتور
        /// </summary>
        public virtual Cart cart { get; set; }
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
        public int colorId { get; set; }
        public int colorPrice { get; set; }
        public int warrentyId { get; set; }
        public int warrentyPrice { get; set; }
    }

    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.ToTable(nameof(CartProduct), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.cartId).IsRequired();
            builder.Property(p => p.qty).IsRequired();
            builder.Property(p => p.shopProductId).IsRequired();
            builder.Property(p => p.price).IsRequired();
            builder.Property(p => p.totalPrice).IsRequired();

            builder.HasOne(o => o.cart).WithMany(m => m.cartProducts).HasForeignKey(f => f.cartId);
            builder.HasOne(o => o.shopProduct).WithMany(m => m.cartProducts).HasForeignKey(f => f.shopProductId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

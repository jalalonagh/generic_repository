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
    public class CartAdditionalCost: BaseEntity<int>
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

    public class CartAdditionalCostConfiguration : IEntityTypeConfiguration<CartAdditionalCost>
    {
        public void Configure(EntityTypeBuilder<CartAdditionalCost> builder)
        {
            builder.ToTable(nameof(CartAdditionalCost), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.cartId).IsRequired();
            builder.Property(p => p.additionalCostId).IsRequired();
            builder.Property(p => p.cost).IsRequired().IsUnicode();

            builder.HasOne(o => o.cart).WithMany(m => m.additionalCosts).HasForeignKey(f => f.cartId);
            builder.HasOne(o => o.additionalCost).WithMany(m => m.cartAdditionalCosts).HasForeignKey(f => f.additionalCostId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

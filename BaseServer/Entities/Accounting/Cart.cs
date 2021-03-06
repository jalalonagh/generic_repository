﻿using Common;
using Common.Utilities;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Accounting
{
    /// <summary>
    /// جدول فاکتور
    /// </summary>
    public class Cart: BaseEntity<int>
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// مدل کاربر
        /// </summary>
        [JsonIgnore]
        public virtual User.User user { get; set; }
        /// <summary>
        /// شناسه آدرس ارسال
        /// </summary>
        public int? addressId { get; set; }
        /// <summary>
        /// مدل آدرس تحویل
        /// </summary>
        public virtual UserAddress address { get; set; }
        /// <summary>
        /// کد فاکتور
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// بارکد
        /// </summary>
        public string barCode { get; set; }
        /// <summary>
        /// کیو آر کد
        /// </summary>
        public string qrCode { get; set; }
        /// <summary>
        /// تاریخ انقضاء
        /// </summary>
        public DateTime expirationTime { get; set; }
        /// <summary>
        /// فهرست کالا ها
        /// </summary>
        public IEnumerable<CartProduct> cartProducts { get; set; }
        /// <summary>
        /// هزینه های اضافی
        /// </summary>
        public virtual IEnumerable<CartAdditionalCost> additionalCosts { get; set; }
    }

    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable(nameof(Cart), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.userId).IsRequired();
            builder.Property(p => p.code).IsRequired().IsUnicode();

            builder.HasOne(o => o.user).WithMany(m => m.carts).HasForeignKey(f => f.userId);
            builder.HasOne(o => o.address).WithMany(m => m.carts).HasForeignKey(f => f.addressId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

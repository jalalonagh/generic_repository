using Common;
using Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text;

namespace Entities.User
{
    /// <summary>
    /// جدول آدرس های کاربر
    /// </summary>
    public class UserAddress: BaseEntity<int>
    {
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// مدل کاربر
        /// </summary>
        public virtual User user { get; set; }
        /// <summary>
        /// کشور
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// شهر
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// منطقه
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// شماره پلاک
        /// </summary>
        public string buildingNo { get; set; }
        /// <summary>
        /// طبقه
        /// </summary>
        public int floor { get; set; }
        /// <summary>
        /// واحد
        /// </summary>
        public string room { get; set; }
        /// <summary>
        /// مختصات x
        /// </summary>
        public double locationLat { get; set; }
        /// <summary>
        /// مختصات y
        /// </summary>
        public double locationLng { get; set; }
        /// <summary>
        /// نوع آدرس
        /// </summary>
        public ADDRESS_TYPE addressType { get; set; } = ADDRESS_TYPE.HOME;
        /// <summary>
        /// فهرست فاکتور های آدرس
        /// </summary>
        public virtual IEnumerable<Invoice> invoices { get; set; }
        /// <summary>
        /// سبد های کالا
        /// </summary>
        public virtual IEnumerable<Cart> carts { get; set; }
    }

    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable(nameof(UserAddress), SchemaEnum.User.ToString());

            builder.Property(p => p.userId).IsRequired();
            builder.Property(p => p.street).IsRequired();
            builder.Property(p => p.buildingNo).IsRequired();
            builder.Property(p => p.room).IsRequired();

            builder.HasOne(o => o.user).WithMany(w => w.addresses).HasForeignKey(h => h.userId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

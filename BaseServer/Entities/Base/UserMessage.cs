
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Base
{
    /// <summary>
    /// جدول پیام های کاربران
    /// </summary>
    public class UserMessage : BaseEntity<int>
    {
        /// <summary>
        /// شناسه کاربر مقصد
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        ///  مدل کاربر مقصد
        /// </summary>
        public virtual User.User user { get; set; }
        /// <summary>
        /// پیام کاربر
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// پیام مرجع
        /// </summary>
        public int parentId { get; set; }
    }

    public class UserMessageConfiguration : IEntityTypeConfiguration<UserMessage>
    {
        public void Configure(EntityTypeBuilder<UserMessage> builder)
        {
            builder.ToTable(nameof(UserMessage), SchemaEnum.Base.ToString());

            builder.Property(p => p.userId).IsRequired();
            builder.Property(p => p.message).IsRequired();

            builder.HasOne(o => o.user).WithMany(w => w.messages).HasForeignKey(h => h.userId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }

}


using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Base
{
    /// <summary>
    /// جدول کامنت های کاربران
    /// </summary>
    public class UserComment : BaseEntity<int>
    {
        /// <summary>
        /// شناسه کاربر مقصد
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// مدل کاربر مقصد
        /// </summary>
        public virtual User.User user { get; set; }
        /// <summary>
        /// امتیاز
        /// </summary>
        public int rate { get; set; } = 3; // of 5
        /// <summary>
        /// توضیحات
        /// </summary>
        public string description { get; set; }
    }

    public class UserCommentConfiguration : IEntityTypeConfiguration<UserComment>
    {
        public void Configure(EntityTypeBuilder<UserComment> builder)
        {
            builder.ToTable(nameof(UserComment), SchemaEnum.Base.ToString());

            builder.Property(p => p.userId).IsRequired();
            builder.Property(p => p.rate).IsRequired();
            builder.Property(p => p.description).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

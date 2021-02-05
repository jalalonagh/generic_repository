using Common.Utilities;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.User
{
    public class Role : IdentityRole<int>, IEntity
    {
        public Role()
        {
            creationPersianDateTime = creationDateTime.ToPersian();
            modifiedPersianDateTime = modifiedDateTime.ToPersian();
            deletedPersianDateTime = deletedDateTime.ToPersian();
            creationDay = creationDateTime.DayOfWeek;
            modifiedDay = modifiedDateTime?.DayOfWeek;
            deletedDay = deletedDateTime?.DayOfWeek;
        }

        /// <summary>
        /// شرح
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// فعال ؟
        /// </summary>
        public bool? isActive { get; set; }
        /// <summary>
        /// حذف شده ؟
        /// </summary>
        public bool? deleted { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime creationDateTime { get; set; }
        /// <summary>
        /// روز ایجاد
        /// </summary>
        public DayOfWeek creationDay { get; set; }
        /// <summary>
        /// تاریخ ایجاد شمسی
        /// </summary>
        public string creationPersianDateTime { get; set; }
        /// <summary>
        /// تاریخ ویرایش
        /// </summary>
        public DateTime? modifiedDateTime { get; set; }
        /// <summary>
        /// روز ویرایش
        /// </summary>
        public DayOfWeek? modifiedDay { get; set; }
        /// <summary>
        /// تاریخ ویرایش به شمسی
        /// </summary>
        public string modifiedPersianDateTime { get; set; }
        /// <summary>
        /// تاریخ حذف منطقی
        /// </summary>
        public DateTime? deletedDateTime { get; set; }
        /// <summary>
        /// روز حذف منطقی
        /// </summary>
        public DayOfWeek? deletedDay { get; set; }
        /// <summary>
        /// تاریخ حذف منطقی شمسی
        /// </summary>
        public string deletedPersianDateTime { get; set; }
        /// <summary>
        /// اولویت
        /// </summary>
        public int? priority { get; set; }
        /// <summary>
        /// اهمیت
        /// </summary>
        public int? important { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public int? userCreatedId { get; set; }
        /// <summary>
        /// تگ های سیستمی
        /// </summary>
        public string systemTag { get; set; }
        /// <summary>
        /// دسته بندی سیستمی
        /// </summary>
        public string systemCategory { get; set; }

        /// <summary>
        /// دسترسی ها
        /// </summary>
        public string access { get; set; }
    }
    

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Name).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

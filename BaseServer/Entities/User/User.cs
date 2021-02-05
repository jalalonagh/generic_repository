using Common;
using Common.Utilities;
using Entities;
using Entities.Accounting;
using Entities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.User
{
    /// <summary>
    /// جدول کاربر
    /// </summary>
    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {
            creationPersianDateTime = creationDateTime.ToPersian();
            modifiedPersianDateTime = modifiedDateTime.ToPersian();
            deletedPersianDateTime = deletedDateTime.ToPersian();
            creationDay = creationDateTime.DayOfWeek;
            modifiedDay = modifiedDateTime?.DayOfWeek;
            deletedDay = deletedDateTime?.DayOfWeek;
            isActive = true;
        }

        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string fullname { get; set; }
        /// <summary>
        /// سن
        /// </summary>
        public int? age { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        public GENDER_TYPE? gender { get; set; }
        /// <summary>
        /// تصویر پرسنلی
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// تاریخ آخرین ورود
        /// </summary>
        public DateTimeOffset? lastLoginDate { get; set; }
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
        /// تلفن منزل
        /// </summary>
        public string homePhone { get; set; }
        /// <summary>
        /// کد ملی
        /// </summary>
        public string nationalId { get; set; }
        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        public string identityNumber { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime? birthday { get; set; }
        /// <summary>
        /// نام پدر
        /// </summary>
        public string fatherName { get; set; }
        /// <summary>
        /// قد
        /// </summary>
        public int? tall { get; set; }
        /// <summary>
        /// وزن
        /// </summary>
        public int? weight { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// شماره شبا
        /// </summary>
        public string irBank { get; set; }
        /// <summary>
        /// کارت ملی
        /// </summary>
        public string identityCard { get; set; }
        /// <summary>
        /// شناسنامه
        /// </summary>
        public string nationalCard { get; set; }
        /// <summary>
        /// دفترچه بیمه
        /// </summary>
        public string insuranceCard { get; set; }
        /// <summary>
        /// شناسه مادر
        /// </summary>
        public int? motherId { get; set; }
        /// <summary>
        /// شناسه پدر
        /// </summary>
        public int? fatherId { get; set; }
        /// <summary>
        /// نام مرکز
        /// </summary>
        public string officeName { get; set; }
        /// <summary>
        /// بیوگرافی
        /// </summary>
        public string bio { get; set; }
        /// <summary>
        /// شناسه معرف
        /// </summary>
        public int representerId { get; set; }

        /// <summary>
        /// پیام های ارسالی
        /// </summary>
        public virtual IEnumerable<UserMessage> messages { get; set; }
        /// <summary>
        /// فهرست کامنت های کاربر
        /// </summary>
        public virtual IEnumerable<UserComment> comments { get; set; }
        /// <summary>
        /// فهرست آدرس های کاربر
        /// </summary>
        public virtual IEnumerable<UserAddress> addresses { get; set; }
        /// <summary>
        /// فهرست بانک ها
        /// </summary>
        public virtual IEnumerable<BankAccount> banks { get; set; }
        /// <summary>
        /// فهرست فاکتور های کاربر
        /// </summary>
        public virtual IEnumerable<Invoice> invoices { get; set; }
        /// <summary>
        /// سبد های کالا
        /// </summary>
        public virtual IEnumerable<Cart> carts { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100).IsUnicode();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

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
    /// جدول حساب بانکی
    /// </summary>
    public class BankAccount: BaseEntity<int>
    {
        /// <summary>
        /// شناسه رکورد
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// رکورد حذف شده ؟
        /// </summary>
        public bool? deleted { get; set; }
        /// <summary>
        /// رکورد فعال ؟
        /// </summary>
        public bool? isActive { get; set; }
        /// <summary>
        /// زمان ایجاد رکورد
        /// </summary>
        public DateTime creationDateTime { get; set; }
        /// <summary>
        /// روز ایجاد رکورد
        /// </summary>
        public DayOfWeek creationDay { get; set; }
        /// <summary>
        /// زمان ایجاد رکورد به شمسی
        /// </summary>
        public string creationPersianDateTime { get; set; }
        /// <summary>
        /// زمان بروزرسانی رکورد
        /// </summary>
        public DateTime? modifiedDateTime { get; set; }
        /// <summary>
        /// روز بروزرسانی رکورد
        /// </summary>
        public DayOfWeek? modifiedDay { get; set; }
        /// <summary>
        /// زمان بروزرسانی رکورد به شمسی
        /// </summary>
        public string modifiedPersianDateTime { get; set; }
        /// <summary>
        /// زمان حذف
        /// </summary>
        public DateTime? deletedDateTime { get; set; }
        /// <summary>
        /// روز حذف
        /// </summary>
        public DayOfWeek? deletedDay { get; set; }
        /// <summary>
        /// زمان حذف به شمسی
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
        /// شناسه کاربر ایجاد کننده
        /// </summary>
        public int? userCreatedId { get; set; }
        /// <summary>
        /// تگ های رکورد
        /// </summary>
        public string systemTag { get; set; }
        /// <summary>
        /// دسته بندی سیستمی
        /// </summary>
        public string systemCategory { get; set; }
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// مدل کاربر
        /// </summary>
        [JsonIgnore]
        public virtual User.User user { get; set; }
        /// <summary>
        /// نام حساب
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// نام بانک
        /// </summary>
        public string bamkName { get; set; }
        /// <summary>
        /// آدرس بانک
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// شماره تماس
        /// </summary>
        public string contact { get; set; }
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string accountNo { get; set; }
        /// <summary>
        /// شماره کارت
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// شماره شباء
        /// </summary>
        public string ibanNo { get; set; }
        /// <summary>
        /// شناسه پرداخت الکترونیکی
        /// </summary>
        public string merchantId { get; set; }
        /// <summary>
        /// شماره موبایل مرتبط
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// ایمیل مرتبط
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// آدرس بازگشت
        /// </summary>
        public string callbackUrl { get; set; }

        /// <summary>
        /// فهرست پرداخت ها
        /// </summary>
        public IEnumerable<PaymentRecipt> recipts { get; set; }
    }

    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable(nameof(BankAccount), SchemaEnum.Accounting.ToString());

            builder.Property(p => p.userId).IsRequired();
            builder.Property(p => p.name).IsRequired();
            builder.Property(p => p.accountNo).IsRequired().IsUnicode();
            builder.Property(p => p.cardNo).IsUnicode().IsRequired();
            builder.Property(p => p.ibanNo).IsUnicode().IsRequired();

            builder.HasOne(o => o.user).WithMany(m => m.banks).HasForeignKey(f => f.userId);

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}

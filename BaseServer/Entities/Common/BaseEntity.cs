using Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public interface IEntity
    {
        /// <summary>
        /// شناسه رکورد
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public BaseEntity()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public int? priority { get; set; } = 1;
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
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}

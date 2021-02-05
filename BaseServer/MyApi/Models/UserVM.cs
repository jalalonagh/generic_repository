using Common;
using Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace Refah.Models
{
    public class UserVM : IValidatableObject
    {
        /// <summary>
        /// شناسه رکورد
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// رکورد حذف شده ؟
        /// </summary>
        public bool? Deleted { get; set; }
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
        public int? Priority { get; set; }
        /// <summary>
        /// اهمیت
        /// </summary>
        public int? Important { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public string Status { get; set; }
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

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public int Age { get; set; }

        public GENDER_TYPE Gender { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام کاربری نمیتواند Test باشد", new[] { nameof(UserName) });
            //if (Password.Equals("123456"))
            //    yield return new ValidationResult("رمز عبور نمیتواند 123456 باشد", new[] { nameof(Password) });
            //if (Gender == GenderType.Male && Age > 30)
            //    yield return new ValidationResult("آقایان بیشتر از 30 سال معتبر نیستند", new[] { nameof(Gender), nameof(Age) });
        }
    }
}

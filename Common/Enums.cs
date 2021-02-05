using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common
{
    public enum PROFILE_TYPE
    {
        [Display(Name = "شخصی")]
        PERSONAL,
        [Display(Name = "شرکتی")]
        COMPANY,
        [Display(Name = "خانوادگی")]
        FAMILY,
        [Display(Name = "مدرسه")]
        SCHOOL,
        [Display(Name = "فروشگاهی")]
        SHOP,
        [Display(Name = "تیمی")]
        TEAM
    }

    public enum TASK_STATUS
    {
        [Display(Name = "تسک روز")]
        SELECTED = 1,
        [Display(Name = "ایجاد شده")]
        CREATED = 1000,
        [Display(Name = "فعال")]
        ACTIVE = 1002,
        [Display(Name = "در حال انجام")]
        INPROCESS = 1003,
        [Display(Name = "اتمام یافته")]
        FINISHED = 1004,
        [Display(Name = "معلق")]
        WAITING = 1005,
        [Display(Name = "لغو شده")]
        CANCELED = 1006
    }

    public enum TASK_IMPORTANT
    {
        [Display(Name = "اهمیت A+")]
        A_PLUS = 4,
        [Display(Name = "اهمیت A")]
        A = 3,
        [Display(Name = "اهمیت B")]
        B = 2,
        [Display(Name = "اهمیت C")]
        C = 1
    }

    public enum TASK_ACTIVITY_TYPE
    {
        [Display(Name = "ایجاد")]
        CREATE,
        [Display(Name = "ویرایش")]
        UPDATE,
        [Display(Name = "نمایش")]
        VIEW,
        [Display(Name = "حذف")]
        REMOVE,
        [Display(Name = "انتقال")]
        REDIRECT,
        [Display(Name = "ثبت کامنت")]
        SET_COMMENT,
        [Display(Name = "بارگزاری عکس")]
        UPLOAD_IMAGE,
        [Display(Name = "بارگزاری فایل")]
        UPLOAD_FILE,
        [Display(Name = "بارگزاری فایل کد")]
        UPLOAD_CODE,
        [Display(Name = "بارگزاری فایل صوتی")]
        UPLOAD_VOICE,
        [Display(Name = "بارگزاری فایل ویدیو")]
        UPLOAD_VIDEO,
        [Display(Name = "تغییر مجری")]
        CHANGE_USER,
        [Display(Name = "تغییر سرپرست")]
        CHANGE_ADMIN,
        [Display(Name = "تغییر تائید کننده")]
        CHANGE_ACCEPTOR,
        [Display(Name = "تغییر کنترل کننده")]
        CHANGE_CONTROLLER,
        [Display(Name = "تغییر کنترل کننده اجرائی")]
        CHANGE_EX_CONTROLLER,
        [Display(Name = "تغییر وضعیت")]
        CHANGE_STATUS,
        [Display(Name = "تغییر نوع تسک")]
        CHANGE_TYPE,
        [Display(Name = "تغییر زمان")]
        CHANGE_TIME,
        [Display(Name = "تعریف زمان")]
        SET_TIME,
        [Display(Name = "تماس با کاربر")]
        CALL_TO_USER
    }

    public enum USER_LEVEL_IN_TASK
    {
        [Display(Name = "مدیر")]
        ADMIN = 100,
        [Display(Name = "سرپرست")]
        MANAGER = 95,
        [Display(Name = "مجری")]
        EXECUTER = 50,
        [Display(Name = "کنترلر")]
        CONTROLLER = 70,
        [Display(Name = "کنترلر دوم")]
        SECOND_CONTROLLER = 80,
        [Display(Name = "پشتیبان")]
        SUPPORTER = 40,
        [Display(Name = "تحلیل گر")]
        ANALYZOR = 30,
        [Display(Name = "تائید کننده")]
        ACCEPTOR = 90
    }

    public enum WORKING_STATUS
    {
        [Display(Name = "فعال")]
        ACTIVE,
        [Display(Name = "غیر فعال")]
        INACTIVE,
        [Display(Name = "نیروی جایگزین")]
        ALTERNATIVE_FORCE,
    }

    public enum JOB_LEVEL
    {
        [Display(Name = "عمومی")]
        GENERAL,
        [Display(Name = "ساده")]
        SIMPLE,
        [Display(Name = "متوسط")]
        MEDIUM,
        [Display(Name = "سنگین")]
        HEAVY,
        [Display(Name = "سخت")]
        HARD,
        [Display(Name = "فنی")]
        TECHNICAL,
        [Display(Name = "سلامت")]
        HEALTHY,
        [Display(Name = "حرفه ای")]
        PROFESIONAL
    }

    public enum GENDER_TYPE
    {
        [Display(Name = "مرد")]
        MALE = 1,
        [Display(Name = "زن")]
        FEMALE = 2
    }

    public enum ADDRESS_TYPE
    {
        [Display(Name = "خانه")]
        HOME,
        [Display(Name = "محل کار")]
        WORK,
        [Display(Name = "منزل اقوام")]
        FAMILY,
        [Display(Name = "اداره")]
        OFFICE,
        [Display(Name = "بیرون")]
        OUT_DOOR,
        [Display(Name = "منزل دوستان")]
        FREINDS
    }

    public enum SchemaEnum
    {
        [Display(Name = "پایه")]
        Base,
        [Display(Name = "فروشگاه")]
        Shop,
        [Display(Name = "مالی")]
        Accounting,
        [Display(Name = "کاربری")]
        User,
        [Display(Name = "مدیریت محتوا")]
        CMS
    }

    public class Enums
    {
    }
}

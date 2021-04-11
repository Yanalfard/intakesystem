using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdUser
    {
        public int UserId { get; set; }
        [DisplayName("نام")]
        public string Name { get; set; }
        [DisplayName("شماره تلفن")]
        public string TellNo { get; set; }
        [DisplayName("کد ملی")]
        public string IdentificationNo { get; set; }
        [DisplayName("رمز عبور")]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("جنسیت")]
        public bool Gender { get; set; }
        [DisplayName("تاریخ ثبت نام")]
        public System.DateTime DateCreated { get; set; }
        [DisplayName("بازه ی ویزیت")]
        public Nullable<short> VisitTime { get; set; }
        [DisplayName("قیمت ویزیت")]
        public long VisitPrice { get; set; }
        [DisplayName("آدرس")]
        public string Address { get; set; }
        public Nullable<int> LocationId { get; set; }
        [DisplayName("تاریخ تولد")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [DisplayName("درباره")]
        public string DoctorDescription { get; set; }
        [DisplayName("وضعیت فعالیت")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(MdUser))]
    public class TblUser
    {

    }
}

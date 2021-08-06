using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class EditDoctorVm
    {
        public int UserId { get; set; }
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]

        public string Name { get; set; }
        [MaxLength(11)]
        [StringLength(11)]
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[Remote("VerifyTellNo", "Account")]
        public string TellNo { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [CodeMelli("لطفا کد ملی را بدرستی وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(10, ErrorMessage = "تعداد کاراکتر کم است")]
        [StringLength(10)]
        //[Remote("VerifyIdentificationNo", "Account")]
        public string IdentificationNo { get; set; }
        [Display(Name = "تخصص ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? SpecialityId { get; set; }
        public string ImageUrl { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string DoctorDescription { get; set; }
    }
    public class RegisterDoctorVm
    {
        public int UserId { get; set; }
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]

        public string Name { get; set; }
        [MaxLength(11)]
        [StringLength(11)]
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[Remote("VerifyTellNo", "Account")]
        public string TellNo { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [CodeMelli("لطفا کد ملی را بدرستی وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(10, ErrorMessage = "تعداد کاراکتر کم است")]
        [StringLength(10)]
        //[Remote("VerifyIdentificationNo", "Account")]
        public string IdentificationNo { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }
        [Display(Name = "تخصص ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? SpecialityId { get; set; }
        public string ImageUrl { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string DoctorDescription { get; set; }
    }
    public class RegisterVm
    {
        public int UserId { get; set; }
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]

        public string Name { get; set; }
        [MaxLength(11)]
        [StringLength(11)]
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[Remote("VerifyTellNo", "Account")]
        public string TellNo { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [CodeMelli("لطفا کد ملی را بدرستی وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(10, ErrorMessage = "تعداد کاراکتر کم است")]
        [StringLength(10)]
        //[Remote("VerifyIdentificationNo", "Account")]
        public string IdentificationNo { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [Display(Name = "کلمه عبور")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        [StringLength(25)]
        public string RePassword { get; set; }
        public int RoleId { get; set; }
        public string ImageUrl { get; set; }
        public int Gender { get; set; }
    }

    public class RegisterUserVm : CaptchaVm
    {
        public int UserId { get; set; }
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]

        public string Name { get; set; }
        [MaxLength(11)]
        [StringLength(11)]
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[Remote("VerifyTellNo", "Account")]
        public string TellNo { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        [StringLength(25)]
        public string RePassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer.ViewModels
{
 
    public class EditUserVm
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
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
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
        public int RoleId { get; set; }
        public int Gender { get; set; }
    }
    public class EditUserInAdminVm
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
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        //[Remote("VerifyTellNo", "Account")]
        public string TellNo { get; set; }
        [Display(Name = "کد ملی")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[CodeMelli("لطفا کد ملی را بدرستی وارد کنید")]
        //[MaxLength(10, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        //[MinLength(10, ErrorMessage = "تعداد کاراکتر کم است")]
        [StringLength(10)]
        //[Remote("VerifyIdentificationNo", "Account")]
        public string IdentificationNo { get; set; }
        public int RoleId { get; set; }
        public int Gender { get; set; }
    }
    public class VmChangePassword
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [Display(Name = "کلمه عبور")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }
    }
   
    public class LoginVm : CaptchaVm
    {
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        [StringLength(11)]
        public string TellNo { get; set; }
        [Display(Name = "کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class CaptchaVm
    {
        public string Captcha { get; set; }
    }

    public class UserProfileVm
    {
        public int UserId { get; set; }

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[CodeMelli("لطفا کد ملی را بدرستی وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(10, ErrorMessage = "تعداد کاراکتر کم است")]
        [StringLength(10)]
        public string IdentificationNo { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(500)]
        public string Address { get; set; }

        public int Gender { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer.ViewModels
{
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
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string ImageUrl { get; set; }
        public int Gender { get; set; }
    }

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
}

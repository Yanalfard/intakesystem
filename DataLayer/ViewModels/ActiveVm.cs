using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class ActiveVm : CaptchaVm
    {
        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(6, ErrorMessage = "کد اشتباه است")]
        [MaxLength(6, ErrorMessage = "کد اشتباه است")]
        [MinLength(6, ErrorMessage = "کد اشتباه است")]
        [DataType(DataType.PhoneNumber)]
        public string Auth { get; set; }
        public string Tell { get; set; }
    }
}

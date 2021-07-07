using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class HospitalVm
    {
        public int HospitalId { get; set; }
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "شهر ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? LocationId { get; set; }
        [Display(Name = "آدرس ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]

        public string Address { get; set; }
        [Display(Name = "نقشه ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Lat { get; set; }
        public string Lon { get; set; }
        [MaxLength(11, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(11)]
        [Display(Name = " تلفن 1")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
       // [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        public string TellNo { get; set; }
        [MaxLength(11, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(11)]
        [Display(Name = "تلفن 2")]
      //  [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        public string TellNo2 { get; set; }
        //public int AdminId { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "دسته ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? CatagoryId { get; set; }
        public List<TblCatagory> TblCatagory { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیحات کوتاه ")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(500)]
        public string AboutUs { get; set; }
        public short Fee { get; set; }
        //public bool IsActive { get; set; }

        public int ostanIdSelect { get; set; }

        public List<TblLocation> TblLocations { get; set; }
    }
}

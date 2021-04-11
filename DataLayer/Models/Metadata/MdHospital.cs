
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{

    public class MdHospital
    {
        public int HospitalId { get; set; }
        [DisplayName("نام")]
        public string Name { get; set; }
        public int LocationId { get; set; }
        [DisplayName("آدرس")]
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        [DisplayName("تلفن 1")]
        public string TellNo { get; set; }
        [DisplayName("تلفن 2")]
        public string TellNo2 { get; set; }
        public int AdminId { get; set; }
        [DisplayName("تاریخ ثبت")]
        public System.DateTime DateCreated { get; set; }
        [DisplayName("عکس")]
        public string ImageUrl { get; set; }
        public int CatagoryId { get; set; }
        [DisplayName("درباره ما")]
        public string AboutUs { get; set; }
        [DisplayName("درصد سود از ویزیت")]
        public short Fee { get; set; }
    }

    [MetadataType(typeof(MdHospital))]
    public class TblHospital
    {

    }
}

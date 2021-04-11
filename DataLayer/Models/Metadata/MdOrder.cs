using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdOrder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int HospitalSpecialityRelId { get; set; }
        [DisplayName("زمان")]
        public System.DateTime Time { get; set; }
        [DisplayName("مقدار")]
        public long Price { get; set; }
        [DisplayName("پرداخت شده؟")]
        public bool IsPayed { get; set; }
        public System.DateTime DateCreated { get; set; }
        [DisplayName("بخش پزشک")]
        public long DoctorsPart { get; set; }
        [DisplayName("بخش مرکز")]
        public long HospitalsPart { get; set; }
        [DisplayName("وضعیت پرداختی به پزشک")]
        public int SettlementStatus { get; set; }
    }

    [MetadataType(typeof(MdOrder))]
    public class TblOrder
    {

    }
}

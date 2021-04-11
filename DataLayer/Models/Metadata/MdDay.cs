
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdDay
    {
        public int DayId { get; set; }
        [DisplayName("???")]
        public Nullable<short> DayOfWeek { get; set; }
        public bool IsWorking { get; set; }
        [DisplayName("???? ????")]
        public Nullable<short> StartShift { get; set; }
        [DisplayName("????? ????")]
        public Nullable<short> EndShift { get; set; }
    }

    [MetadataType(typeof(MdDay))]
    public class TblDay
    {

    }
}

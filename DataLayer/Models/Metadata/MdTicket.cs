using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdTicket
    {
        public int TicketId { get; set; }
        [DisplayName("عنوان")]
        public string Title { get; set; }
        [DisplayName("بدنه")]
        public string Body { get; set; }
        [DisplayName("تاریخ ساخت")]
        public System.DateTime DateCreated { get; set; }
        public int FromId { get; set; }
        public int HospitalId { get; set; }
        public Nullable<int> ParentId { get; set; }
        [DisplayName("عکس")]
        public string ImageUrl { get; set; }
    }
    
    [MetadataType(typeof(MdTicket))]
    public class TblTicket
    {

    }
}

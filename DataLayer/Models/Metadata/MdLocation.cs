
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdLocation
    {
        public int LocationId { get; set; }
        [DisplayName("??? ????")]
        public string LocationName { get; set; }
        public Nullable<int> LocationParentId { get; set; }
    }

    [MetadataType(typeof(MdLocation))]
    public class TblLocation
    {

    }
}

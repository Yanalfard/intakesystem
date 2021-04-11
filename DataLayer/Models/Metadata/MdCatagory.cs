
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdCatagory
    {
        public int CatagoryId { get; set; }
        public string Name { get; set; }
    }

    [MetadataType(typeof(MdCatagory))]
    public class TblCatagory
    {

    }
}

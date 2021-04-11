using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdImage
    {
        public int ImageId { get; set; }
        [DisplayName("عکس")]
        public string ImageUrl { get; set; }
        [DisplayName("نام")]
        public string Name { get; set; }
        public int HospitalId { get; set; }
    
    }

    [MetadataType(typeof(MdImage))]
    public class TblImage
    {

    }
}

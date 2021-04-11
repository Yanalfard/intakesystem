using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdSpeciality
    {
        public int SpecialityId { get; set; }
        [DisplayName("نام تخصص")]
        public string Name { get; set; }
        [DisplayName("وضعیت فعالیت")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(MdSpeciality))]
    public class TblSpeciality
    {

    }
}

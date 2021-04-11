using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdRole
    {
        public int RoleId { get; set; }
        [DisplayName("نام")]
        public string Title { get; set; }
        [DisplayName("نام سیستمی")]
        public string Name { get; set; }
    }

    [MetadataType(typeof(MdRole))]
    public class TblRole
    {

    }
}

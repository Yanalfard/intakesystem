using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdConfig
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    [MetadataType(typeof(MdConfig))]
    public class TblConfig
    {

    }
}

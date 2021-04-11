
using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Metadata
{
    public class MdHospitalSpecialityRel
    {
        public int HospitalSpecialityRelId { get; set; }
        public int HospitalId { get; set; }
        public int SpecialityId { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public Nullable<int> DayId { get; set; }
        public bool IsDeleted { get; set; }
    }

    [MetadataType(typeof(MdHospitalSpecialityRel))]
    public class TblHospitalSpecialityRel
    {

    }
}

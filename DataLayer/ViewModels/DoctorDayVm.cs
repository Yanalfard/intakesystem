using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class DoctorDayVm
    {
        public List<TblHospitalSpecialityRel> TblHospitalSpecialityRels { get; set; }
        public List<TblDay> MyProperty { get; set; }
    }
}

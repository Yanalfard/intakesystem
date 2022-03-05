﻿using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VisitDayViewModel
    {
        public TblHospitalSpecialityRel TblHospitalSpecialityRel { get; set; }
        public TblDay TblDay { get; set; }
        public bool IsValid { get; set; }
        public string DayVisit { get; set; }
        public string DayName { get; set; }
        public string Text { get; set; }
        public string SpecialityName { get; set; }
        public string DoctorName { get; set; }
        public string DoctorImage { get; set; }

    }
}

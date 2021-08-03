using DataLayer.Models;
using Services.Repositories;
using System;

namespace Services.Services
{
    public class Core: IDisposable
    {
        private readonly IntakeSystemEntities _db = new IntakeSystemEntities();

        private MainRepo<TblLocation> _location;
        private MainRepo<TblRole> _role;
        private MainRepo<TblCatagory> _catagory;
        private MainRepo<TblDay> _day;
        private MainRepo<TblSpeciality> _speciality;
        private MainRepo<TblUser> _user;
        private MainRepo<TblTicket> _ticket;
        private MainRepo<TblHospitalSpecialityRel> _hospitalSpecialityRel;
        private MainRepo<TblHosSpecDayRel> _hosSpecDayRel;
        private MainRepo<TblHospital> _hospital;
        private MainRepo<TblImage> _image;
        private MainRepo<TblConfig> _config;
        private MainRepo<TblOrder> _order;

        public MainRepo<TblLocation> Location => _location ?? (_location = new MainRepo<TblLocation>(_db));
        public MainRepo<TblRole> Role => _role ?? (_role = new MainRepo<TblRole>(_db));
        public MainRepo<TblCatagory> Catagory => _catagory ?? (_catagory = new MainRepo<TblCatagory>(_db));
        public MainRepo<TblDay> Day => _day ?? (_day = new MainRepo<TblDay>(_db));
        public MainRepo<TblSpeciality> Speciality => _speciality ?? (_speciality = new MainRepo<TblSpeciality>(_db));
        public MainRepo<TblUser> User => _user ?? (_user = new MainRepo<TblUser>(_db));
        public MainRepo<TblTicket> Ticket => _ticket ?? (_ticket = new MainRepo<TblTicket>(_db));
        public MainRepo<TblHospitalSpecialityRel> HospitalSpecialityRel => _hospitalSpecialityRel ?? (_hospitalSpecialityRel = new MainRepo<TblHospitalSpecialityRel>(_db));
        public MainRepo<TblHosSpecDayRel> HosSpecDayRel => _hosSpecDayRel ?? (_hosSpecDayRel = new MainRepo<TblHosSpecDayRel>(_db));
        public MainRepo<TblHospital> Hospital => _hospital ?? (_hospital = new MainRepo<TblHospital>(_db));
        public MainRepo<TblImage> Image => _image ?? (_image = new MainRepo<TblImage>(_db));
        public MainRepo<TblConfig> Config => _config ?? (_config = new MainRepo<TblConfig>(_db));
        public MainRepo<TblOrder> Order => _order ?? (_order = new MainRepo<TblOrder>(_db));

        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save() => _db.SaveChanges();
    }
}

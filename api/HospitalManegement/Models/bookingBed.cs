using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManegement.Models
{
    public class bookingBed
    {
        public string patientid { set; get; }
        public Patient patient { set; get; }

        public string bedid { set; get; }
        public Bed bed { set; get; }
        public Hospitals Hospitals { set; get; }
    }
}

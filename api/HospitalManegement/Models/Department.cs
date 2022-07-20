using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManegement.Models
{
    public class Department
    {
      
        public int id { set; get; }
        public string name { set; get; }
        public List<Doctor>doctors { set; get; }
      
        public Hospitals Hospitals { set; get; }

        public string image { set; get; }


    }
}

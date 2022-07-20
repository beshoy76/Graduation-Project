using System;
using System.Collections.Generic;

#nullable disable

namespace HospitalManegement.Models
{
    public partial class Bed
    {
        public string id{ get; set; }
      
        public string Bedfloor { set; get; }
        public int bednumber { set; get; }
        public bool available { set; get; }


        public Hospitals Hospitals { set; get; }




    }
}

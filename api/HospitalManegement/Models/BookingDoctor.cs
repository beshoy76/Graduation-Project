using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManegement.Models
{
    public class BookingDoctor
    {
  
   
        public string doctorid { set; get; }
        public Doctor doctor { set; get; }
        [Required]
        public string patientid { set; get; }
        [NotMapped]
        public Patient patient { set; get; }
        public DateTime reservedate { set; get; }
       
        public string prescription { set; get; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HospitalManegement.Models
{
    public  class Doctor
    {
       
        public string id { get; set; }
        public string UseryId { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
        public string name { get; set; }
        public string DLastName { get; set; }
        public string DPhone { get; set; }
        public string ImagePath { get; set; }
        public string DPass { get; set; }
        public string DGender { get; set; }
        public DateTime? DBd { get; set; }
        public int? DAge { get; set; }
        public string DAddress { get; set; }
        public int? DidId { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }



        public ICollection<Patient>patients { set; get; }
        public List<BookingDoctor>bookingDoctors { set; get; }


        public Department department { set; get; }
    
        public Hospitals Hospitals { set; get; }

    
   
       
    }
}

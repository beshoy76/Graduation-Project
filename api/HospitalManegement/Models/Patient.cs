using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HospitalManegement.Models
{
    

    [Index (nameof(PFirstName))]
    public class Patient
    {
       
       
        public string id{ get; set; }
       [ForeignKey("UserId")]
        public string UserxId { get; set; }
      public virtual ApplicationUser AppUser { get; set; }
     
        public string PFirstName { get; set; }
        public string PLastName { get; set; }
       
        public string PGender { get; set; }
      
        public int? PAge { get; set; }

   
        public Hospitals Hospitals { set; get; }

        public ICollection<Doctor>doctors { set; get; }
        public List<BookingDoctor>bookingDoctors { set; get; }


        //beshoy

        public List<medicine> medicines { set; get; }

        public List<prescrption> Prescrptions { set; get; }

        


    }
}

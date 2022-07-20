using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HospitalManegement.Models
{
    public partial class medicine
    {
      
        [Key]
        public int MedicineId { get; set; }

        public String medicineName { set; get; }

        public String ExprDate { get; set; }

        public string Phone { set; get; }

        public string Quantity { get; set; }

        [ForeignKey("patients")]
        public string patientid { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual Patient patients { set; get; }

    }
}

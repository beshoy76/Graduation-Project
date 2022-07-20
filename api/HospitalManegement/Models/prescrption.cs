using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HospitalManegement.Models
{
    public partial class prescrption
    {
      //  [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [Key]
       // [JsonIgnore]
        public int Id { get; set; }
       // [NotMapped]
        public string medicineName { set; get; }
        public string dateTime { get; set; }
        public string department { set; get; }
        public string notes { set; get; }

        [ForeignKey("patients")]
        public string patientid { get; set; }
        [JsonIgnore]
        public virtual Patient patients { set; get; }

    }
}

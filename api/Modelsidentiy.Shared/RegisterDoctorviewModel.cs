using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelsidentiy.Shared
{
    public class RegisterDoctorviewModel
    {
 
        public string Email { get; set; }

        public string Passward { get; set; }

        public string day { get; set; }
        public string Confirmpassward { get; set; }
  

      
        public string DFirstName { get; set; }
        public string DLastName { get; set; }
        public string DPhone { get; set; }
  
      
        public string DGender { get; set; }
        public int departmentid { get; set; }
        public string Hospitalid { get; set; }

        public int? DAge { get; set; }
        public string DAddress
        {
            get; set;
        }
    }
}

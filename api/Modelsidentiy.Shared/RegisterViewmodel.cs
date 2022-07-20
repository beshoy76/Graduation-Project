using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelsidentiy.Shared
{
    public class RegisterViewmodel
    {
      
        public string Email { get; set; }
     
        public string Passward { get; set; }
    
        public string Confirmpassward { get; set; }

    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string PhoneNumper { get; set; }
    }
}

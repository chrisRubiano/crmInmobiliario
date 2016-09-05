using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class DomiciliosMeta
    {

         [Display(Name = "Num Exterior")]        
        public string NumExterior { get; set; }

         [Display(Name = "Num Interior")]        
        public string NumInterior { get; set; }

         [Display(Name = "Entre/Esquina")]        
        public string EntreEsquina { get; set; }

         [Display(Name = "Y Calle")]        
        public string YCalle { get; set; }

    }
}
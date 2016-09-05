using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class MedioContactoMeta
    {

        [Display(Name = "Medio de Contacto")]        
        public string MedioContacto { get; set; }

    }
}
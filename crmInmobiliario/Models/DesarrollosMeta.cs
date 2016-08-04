using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class DesarrollosMeta
    {
        [StringLength(150)]
        public string Desarrollo { get; set; }

        [StringLength(2)]
        public string Clave { get; set; }
    
        [Display(Name = "% Descuento")]        
        public Nullable<decimal> Descuento { get; set; }
        
        [Display(Name = "Cajones de Estacionamiento")]
        public Nullable<int> CajonesEstacionamiento { get; set; }
        
    }
}
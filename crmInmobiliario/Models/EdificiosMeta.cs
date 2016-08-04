using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class EdificiosMeta
    {
        [StringLength(100)]
        public string Desarrollo { get; set; }

        [Display(Name = "% Descuento")]
        public Nullable<decimal> Descuento { get; set; }

        [Display(Name = "Cajones de Estacionamiento")]
        public Nullable<int> CajonesEstacionamiento { get; set; }

    }
}
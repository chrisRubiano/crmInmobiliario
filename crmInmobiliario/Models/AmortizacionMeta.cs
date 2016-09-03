using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class AmortizacionMeta
    {
        [Display(Name = "Fecha Programada")]
        public Nullable<System.DateTime> FechaProgramado { get; set; }
        [Display(Name = "Está Pagado")]
        public Nullable<bool> EstaPagado { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class PropiedadesTipoBanioMeta
    {
        [Display(Name = "Tipo de baños")]
        public string TipoBanio { get; set; }
    }
}
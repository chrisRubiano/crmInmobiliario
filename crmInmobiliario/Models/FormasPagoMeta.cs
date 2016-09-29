using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class FormasPagoMeta
    {
        [Display(Name = "Forma de Pago")]
        [Required(ErrorMessage = "Debe Capturar la Forma de Pago")]
        [StringLength(50)]
        public string FormaPago { get; set; }

        [Required(ErrorMessage = "Debe Capturar el nombre de la base de datos ERP")]
        [StringLength(50)]
        public string ERP { get; set; }

        [StringLength(50)]
        public string ERP2 { get; set; }

    }
}
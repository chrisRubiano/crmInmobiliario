using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class DesarrollosMeta
    {
        [Required]
        [StringLength(150)]
        public string Desarrollo { get; set; }

        [Required(ErrorMessage = "Debe Caoturar la Clave del Desarrollo")]
        [StringLength(2)]
        public string Clave { get; set; }
    
        
        [Required(ErrorMessage = "Debe Capturar el % de Descuento Máximo")]
        [Display(Name = "% Descuento")]        
        public Nullable<decimal> Descuento { get; set; }
        
        [Display(Name = "Cajones de Estacionamiento")]
        public Nullable<int> CajonesEstacionamiento { get; set; }

        [Display(Name = "Fecha de Entrega")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaEntrega { get; set; }
        
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class AmortizacionMeta
    {
        [Display(Name = "Fecha Programada")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaProgramado { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero válido")]
        ////[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public Nullable<decimal> Importe { get; set; }

        [Display(Name = "Está Pagado")]
        public Nullable<bool> EstaPagado { get; set; }

        [Display(Name = "Cotización")]
        public Nullable<int> Cotizacion { get; set; }

    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class CotizacionesMeta
    {
        [Display(Name = "Num Cotización")]
        public int IdCotizacion { get; set; }

        [Display(Name = "Propiedad")]
        [Required(ErrorMessage = "Debe seleccionar una propiedad")]
        public Nullable<int> Propiedad { get; set; }
        [Display(Name = "Prospecto / Cliente")]
        [Required(ErrorMessage = "Debe seleccionar un Cliente / Prospecto")]
        public Nullable<int> Persona { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Cotización")]
        public Nullable<System.DateTime> FechaCotizacion { get; set; }
        [Display(Name = "Precio Final de Venta")]
        [Required(ErrorMessage = "Debe capturar un Precio de Venta")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioFinalVenta { get; set; }
        [Display(Name = "% Enganche")]
        [Range(1.0, 30.0,
            ErrorMessage = "Debe ser máximo 30%")]
        public Nullable<decimal> PorcentajeEnganche { get; set; }

        [DataType(DataType.Currency)]
        public Nullable<decimal> Enganche { get; set; }

        [Range(1, 24,
            ErrorMessage = "Debe ser máxim0 24 parcialidades")]
        public Nullable<int> Parcialidades { get; set; }
        [Display(Name = "% Mensaualidades")]
        public Nullable<decimal> PorcentajeMensualidades { get; set; }
        [Display(Name = "Pago Mensual")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PagoMensual { get; set; }
        public string Vendedor { get; set; }

        public virtual Personas Personas { get; set; }
        public virtual Propiedades Propiedades { get; set; }
    }
}
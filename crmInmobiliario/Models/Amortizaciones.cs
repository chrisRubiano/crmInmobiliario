//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace crmInmobiliario.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Amortizaciones
    {
        public int IdAmortizacion { get; set; }
        public Nullable<int> TipoPago { get; set; }
        public Nullable<int> Persona { get; set; }
        public Nullable<int> Propiedad { get; set; }
        public Nullable<int> Cotizacion { get; set; }
        public Nullable<System.DateTime> FechaProgramado { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public Nullable<bool> EstaPagado { get; set; }
        public string Tipo { get; set; }
    
        public virtual TiposPago TiposPago { get; set; }
    }
}

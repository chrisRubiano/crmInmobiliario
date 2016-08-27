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
    
    public partial class Pagos
    {
        public int IdPago { get; set; }
        public Nullable<int> Tipo { get; set; }
        public Nullable<int> Propiedad { get; set; }
        public Nullable<int> Persona { get; set; }
        public Nullable<int> Cotizacion { get; set; }
        public Nullable<int> Amortizacion { get; set; }
        public Nullable<System.DateTime> FechaPago { get; set; }
        public Nullable<int> Moneda { get; set; }
        public Nullable<decimal> TipoCambio { get; set; }
        public Nullable<decimal> Importe { get; set; }
    
        public virtual Amortizaciones Amortizaciones { get; set; }
        public virtual Cotizaciones Cotizaciones { get; set; }
        public virtual Monedas Monedas { get; set; }
        public virtual Personas Personas { get; set; }
        public virtual Propiedades Propiedades { get; set; }
        public virtual TiposPago TiposPago { get; set; }
    }
}
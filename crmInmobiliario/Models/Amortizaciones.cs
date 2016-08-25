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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Amortizaciones()
        {
            this.Pagos = new HashSet<Pagos>();
        }
    
        public int IdAmortizacion { get; set; }
        public Nullable<int> TipoPago { get; set; }
        public Nullable<int> Persona { get; set; }
        public Nullable<int> Propiedad { get; set; }
        public Nullable<int> Cotizacion { get; set; }
        public Nullable<System.DateTime> FechaProgramado { get; set; }
        public Nullable<int> Moneda { get; set; }
        public Nullable<decimal> TipoCambio { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public Nullable<bool> EstaPagado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}

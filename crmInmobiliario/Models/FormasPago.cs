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
    using System.ComponentModel.DataAnnotations;

<<<<<<< HEAD

=======
>>>>>>> 6ad52d6bf7ad00a1cfaedb00f952f1256c5aee59
    [MetadataType(typeof(FormasPagoMeta))]
    public partial class FormasPago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormasPago()
        {
            this.Pagos = new HashSet<Pagos>();
        }
    
        public int IdFormaPago { get; set; }
        public string FormaPago { get; set; }
        public string ERP { get; set; }
        public string ERP2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}

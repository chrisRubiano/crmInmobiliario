namespace crmInmobiliario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FormasPagoMeta
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public FormasPago()
        //{
        //    this.Pagos = new HashSet<Pagos>();
        //}

        public int IdFormaPago { get; set; }
        [Display(Name = "Forma de pago")]
        public string FormaPago { get; set; }
        public string ERP { get; set; }
        public string ERP2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
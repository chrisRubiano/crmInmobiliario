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
    
    public partial class MediosContacto
    {
        public MediosContacto()
        {
            this.Personas = new HashSet<Personas>();
        }
    
        public int IdMedioContacto { get; set; }
        [Display(Name="Medio de contacto")]
        public string MedioContacto { get; set; }
    
        public virtual ICollection<Personas> Personas { get; set; }
    }
}

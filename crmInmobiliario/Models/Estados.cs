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
    
    public partial class Estados
    {
        public Estados()
        {
            this.Domicilios = new HashSet<Domicilios>();
        }
    
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string Abreviacion { get; set; }
    
        public virtual ICollection<Domicilios> Domicilios { get; set; }
    }
}

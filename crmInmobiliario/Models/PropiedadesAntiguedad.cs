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
    
    public partial class PropiedadesAntiguedad
    {
        public PropiedadesAntiguedad()
        {
            this.Propiedades = new HashSet<Propiedades>();
        }
    
        public int IdAntiguedad { get; set; }
        public string Antiguedad { get; set; }
    
        public virtual ICollection<Propiedades> Propiedades { get; set; }
    }
}

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
    
    public partial class Fotografias
    {
        public int IdFotografia { get; set; }
        public Nullable<int> Propiedad { get; set; }
        public byte[] Fotografia { get; set; }
        public Nullable<bool> Principal { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
    
        public virtual Propiedades Propiedades { get; set; }
    }
}
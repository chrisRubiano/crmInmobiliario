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
    
    public partial class Bitacora
    {
        public int IdBitacora { get; set; }
        public string Evento { get; set; }
        public Nullable<System.DateTime> FechaHora { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string IP { get; set; }
        public Nullable<int> TipoEvento { get; set; }
    }
}

namespace crmInmobiliario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProspectosIncidenciasMeta
    {
        public int Id { get; set; }
        public Nullable<int> Prospecto { get; set; }

        [Display(Name = "Usuario registro")]
        public string UsuarioRegistro { get; set; }

        [Display(Name = "Usuario de incidencia")]
        public string UsuarioIncidencia { get; set; }

        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> FechaRegistro { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetUsers AspNetUsers1 { get; set; }
        public virtual Personas Personas { get; set; }
    }
}
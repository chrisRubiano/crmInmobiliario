using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class Persona
    {
        [Required]
        public int Tipo { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        public string Paterno { get; set; }

        [Display(Name = "Apellido Materno")]
        public string Materno { get; set; }
        public Nullable<int> Genero { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaNacimiento { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Segundo Email")]
        [DataType(DataType.EmailAddress)]
        public string Email2 { get; set; }

        [Display(Name = "Medio de Contacto")]
        public Nullable<int> MedioContacto { get; set; }

        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }

        [Display(Name = "Interés")]
        public Nullable<int> Interes { get; set; }

        [Display(Name = "Especifique interés")]
        [DataType(DataType.MultilineText)]
        public string InteresEspecifique { get; set; }

        [Display(Name = "Código")]
        public string CodigoPersona { get; set; }

        [Display(Name = "Giro de la empresa")]
        [DataType(DataType.MultilineText)]
        public string Giro { get; set; }

        public string nombreCompleto()
        {
            return Nombre + " " + Paterno + " " + Materno;
        }
    }
}
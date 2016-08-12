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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaNacimiento { get; set; }

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

        [Display(Name = "Categoría de interés")]
        public Nullable<int> CategoriaInteres { get; set; }

        public string nombreCompleto()
        {
            return Nombre + " " + Paterno + " " + Materno;
        }
    }
}
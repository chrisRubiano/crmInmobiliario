using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class VariosModelos
    {
        public Personas personas { get; set; }
        public IEnumerable<crmInmobiliario.Models.Notas> notas { get; set; }
        public IEnumerable<crmInmobiliario.Models.Domicilios> domicilios { get; set; }
    }
}
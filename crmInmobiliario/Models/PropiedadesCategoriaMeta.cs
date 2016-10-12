using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PropiedadesCategoriaMeta
    {
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        [Required, StringLength(1)]
        public string Clave { get; set; }
        public Nullable<bool> Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Propiedades> Propiedades { get; set; }
    }
}

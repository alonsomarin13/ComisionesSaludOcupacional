//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComisionesSaludOcupacional.Models.ET01
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comision()
        {
            this.Representante = new HashSet<Representante>();
        }
    
        public int idComision { get; set; }
        public string contacto { get; set; }
        public string contactoCorreo { get; set; }
        public string contactoTelefono { get; set; }
        public string jefatura { get; set; }
        public string jefaturaCorreo { get; set; }
        public string jefaturaTelefono { get; set; }
        public string numeroDeRegistro { get; set; }
        public Nullable<System.DateTime> fechaDeRegistro { get; set; }
        public int idCentroDeTrabajo { get; set; }
        public int idCuenta { get; set; }
        public Nullable<System.DateTime> ultimoInforme { get; set; }
    
        public virtual CentroDeTrabajo CentroDeTrabajo { get; set; }
        public virtual Cuenta Cuenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Representante> Representante { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SaludOcupacionalEntities : DbContext
    {
        public SaludOcupacionalEntities()
            : base("name=SaludOcupacionalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Comision> Comision { get; set; }
        public virtual DbSet<Representante> Representante { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tarea2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class testEntities : DbContext
    {
        public testEntities()
            : base("name=testEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AUTO> AUTO { get; set; }
        public virtual DbSet<CARGOS> CARGOS { get; set; }
        public virtual DbSet<MARCA> MARCA { get; set; }
        public virtual DbSet<MODELO> MODELO { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<TRABAJADOR> TRABAJADOR { get; set; }
    
        public virtual int SP_INSERTAR_PERSONA(string rut, string nombre, string apellido, Nullable<int> edad)
        {
            var rutParameter = rut != null ?
                new ObjectParameter("rut", rut) :
                new ObjectParameter("rut", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var apellidoParameter = apellido != null ?
                new ObjectParameter("apellido", apellido) :
                new ObjectParameter("apellido", typeof(string));
    
            var edadParameter = edad.HasValue ?
                new ObjectParameter("edad", edad) :
                new ObjectParameter("edad", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_INSERTAR_PERSONA", rutParameter, nombreParameter, apellidoParameter, edadParameter);
        }
    
        public virtual ObjectResult<SP_LISTAR_PERSONAS_Result> SP_LISTAR_PERSONAS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_LISTAR_PERSONAS_Result>("SP_LISTAR_PERSONAS");
        }
    }
}

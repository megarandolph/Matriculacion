//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Matriculacion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Imparte
    {
        public int ImparteId { get; set; }
        public Nullable<int> ProfesorId { get; set; }
        public Nullable<int> MateriaId { get; set; }
    
        public virtual Materia Materia { get; set; }
        public virtual Profesor Profesor { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Reserva.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Municao : Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Municao()
        {
            this.Armamentos = new HashSet<Armamento>();
        }
    
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int CalibreId { get; set; }
        public int QuantidadeBala { get; set; }
    
        public virtual Calibre Calibre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Armamento> Armamentos { get; set; }
    }
}

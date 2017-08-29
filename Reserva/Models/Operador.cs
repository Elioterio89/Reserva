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

    public partial class Operador : Militar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Operador()
        {
            this.Cautelas = new HashSet<Cautela>();
        }

        public string Email { get; set; }
        public string AutenticacaoID { get; set; }
        public bool ADM { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int CautelaId { get; set; }

        public virtual Almoxarifado Almoxarifado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cautela> Cautelas { get; set; }

        public string NomeDeGuerra
        {
            get
            {
                return Patente.Sigla + " " + NomeGuerra;
            }
            set { }
        }
    }
}

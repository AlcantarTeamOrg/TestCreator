//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestCreatorWebSite.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Testy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Testy()
        {
            this.Pytania = new HashSet<Pytania>();
        }
    
        public int id_test { get; set; }
        public string nazwa { get; set; }
        public Nullable<System.DateTime> data_stworzenia { get; set; }
        public bool is_visible { get; set; }
        public Nullable<int> id_stanowisko { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pytania> Pytania { get; set; }
        public virtual Stanowiska Stanowiska { get; set; }
    }
}

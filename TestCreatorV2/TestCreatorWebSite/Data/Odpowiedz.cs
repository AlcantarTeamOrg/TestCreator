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
    
    public partial class Odpowiedz
    {
        public int id_odpowiedz { get; set; }
        public int id_pytanie { get; set; }
        public string tresc_odpowiedzi { get; set; }
        public bool is_visible { get; set; }
        public bool dobra { get; set; }
    
        public virtual Pytania Pytania { get; set; }
    }
}

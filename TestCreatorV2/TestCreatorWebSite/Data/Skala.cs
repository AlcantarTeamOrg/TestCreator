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
    
    public partial class Skala
    {
        public int is_skala { get; set; }
        public decimal minimum { get; set; }
        public decimal maksimum { get; set; }
        public int id_odpowiedz { get; set; }
    
        public virtual Odpowiedz Odpowiedz { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCreatorWebSite.Models
{
    public class PytaniaCustom
    {
        public int id_pytanie { get; set; }
        public string tresc_pytania { get; set; }
        public bool is_visible { get; set; }
        public int id_test { get; set; }
        public List<Odpowiedz> Odpowiedz {get; set;}
        public int typ { get; set; } //1 - jednoktotny wybur 2 - wielokrotny 3 - liczbowy 4 - otwarte 5 - skala
    }
}
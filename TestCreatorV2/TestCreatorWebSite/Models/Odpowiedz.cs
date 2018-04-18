using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCreatorWebSite.Models
{
    public class Odpowiedz
    {
        public int id_odpowiedz { get; set; }
        public int id_pytanie { get; set; }
        public string tresc_odpowiedzi { get; set; }
        public bool is_visible { get; set; }
        public bool dobra { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCreatorV2.Models
{
    public class Uzytkownik
    {
        public long IdUser { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }
        public string ReHaslo { get; set; }
        public bool IsVisible { get; set; }

        public int Rola { get; set; }

        public string DataDodania { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }

    }
}
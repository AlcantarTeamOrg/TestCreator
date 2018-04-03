using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCreator.Models
{
    public class Pytanie
    {
        public long id_pytanie { get; set; }
        public string pytanie { get; set; }
        public string odpowiedz { get; set; }
        public int typ { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCreatorWebSite.Models
{
    public class TestSave
    {
        public string Name { get; set; }
        public int id_stanowisko { get; set; }
        public List<PytaniaCustom> listPytania { get; set; }
    }
}
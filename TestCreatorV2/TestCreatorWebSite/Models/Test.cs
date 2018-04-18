using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCreatorWebSite.Models
{
    public class Test
    {

        public int id_test { get; set; }
        public string nazwa { get; set; }
        public Nullable<System.DateTime> data_stworzenia { get; set; }
        public bool is_visible { get; set; }
        public Nullable<int> id_stanowisko { get; set; }

    }
}
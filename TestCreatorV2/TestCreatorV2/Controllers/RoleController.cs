using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestCreatorV2.Data;
using TestCreatorV2.Models;

namespace TestCreatorV2.Controllers
{
    public class RoleController : ApiController
    {
        private List<Rola> roleList = new List<Rola>();
        public List<Rola> GetRole()
        {
            using (var dbContext = new TestCreatorEntities())
            {
                var rol = dbContext.Role.ToList();

                foreach (var item in rol)
                {
                    roleList.Add(new Rola()
                    {
                        IDRola = item.id_rola,
                        NazwaRoli = item.nazwa,
                        IsVisible = item.is_visible
                    });


                }
                return roleList;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestCreatorWebSite.Data;

namespace TestCreatorWebSite.Api
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        public Testy GetTestForID(long id)
        {
            try
            {
                TestCreatorEntities db = new TestCreatorEntities();
                Testy testy = db.Testy.Find(id);
                return testy;
            }
            catch
            {
                return null;
            }

        }
        [HttpGet]
        public Testy qwe()
        {
            try
            {
                TestCreatorEntities db = new TestCreatorEntities();
                Testy testy = db.Testy.Find(10);
                return testy;
            }
            catch
            {
                return null;
            }

        }
    }
}
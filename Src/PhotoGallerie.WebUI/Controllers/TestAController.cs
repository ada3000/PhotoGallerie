using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PhotoGalerie.Controllers
{
    public class TestAController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value "+id;
        }

        [HttpGet]
        public IEnumerable<string> Test1()
        {
            return new string[] { "value1 t", "value2 t" };
        }

        // GET api/<controller>/5
        [HttpGet]
        public string Test2(int id)
        {
            return "value t " + id;
        }
    }
}

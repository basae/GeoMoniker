using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiGeoMoniker.Controllers
{
    public class MapController : ApiController
    {
        // GET api/map
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/map/5
        public string Get(string id)
        {
            return "value";
        }

        // POST api/map
        public void Post([FromBody]string value)
        {
        }

        // PUT api/map/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/map/5
        public void Delete(int id)
        {
        }
    }
}

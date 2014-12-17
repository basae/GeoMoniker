using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiGeoMoniker.Models;

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
        public string Post([FromBody]IEnumerable<Alumnos> Alumnos)
        {
            if (Alumnos == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Objeto Vacio"));
            return "Peticion Correcta";

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

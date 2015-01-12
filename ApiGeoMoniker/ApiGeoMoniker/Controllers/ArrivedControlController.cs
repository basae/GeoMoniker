using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using Business;

namespace ApiGeoMoniker.Controllers
{
    public class ArrivedControlController : ApiController
    {
        private ArrivedControlBusinessLayer _repository = new ArrivedControlBusinessLayer();
        // GET api/arrivedcontrol
        public IEnumerable<ArrivedControl> Get()
        {
            if (!Request.Headers.Contains("DateControl"))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Falta Cabecera 'DateControl'"));
            DateTime _DateControl;
            if (!DateTime.TryParse(Request.Headers.GetValues("DateControl").FirstOrDefault(), out _DateControl))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error en la fecha"));
            ServiceResponse<ArrivedControl> Response = new ServiceResponse<ArrivedControl>();
            Response = _repository.GetByDateControl(_DateControl);
            if(Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));

            return Response.List;
        }

        // GET api/arrivedcontrol/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/arrivedcontrol
        public void Post([FromBody]string value)
        {
        }

        // PUT api/arrivedcontrol/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/arrivedcontrol/5
        public void Delete(int id)
        {
        }
    }
}

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
    public class RouteController : ApiController
    {
        // GET api/route
        private RouteBusinessLayer _routeBusiness = new RouteBusinessLayer();
        public IEnumerable<RouteOuput> Get()
        {
            if (!this.Request.Headers.Contains("IdCompany"))
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Falta Header IdCompany"));
            long IdCompany = Convert.ToInt64(Request.Headers.GetValues("IdCompany").FirstOrDefault());
            ServiceResponse<RouteOuput> Response = new ServiceResponse<RouteOuput>();
            Response = _routeBusiness.SelectByCompany(IdCompany);
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.List;
        }

        // GET api/route/5
        public RouteOuput Get(long id)
        {
            ServiceResponse<Route> Response = new ServiceResponse<Route>();
            Response = _routeBusiness.SelectById(id);
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (RouteOuput)Response.ObjectResult;
        }

        // POST api/route
        public long Post([FromBody]Route _Route)
        {
            ServiceResponse<Route> Response = new ServiceResponse<Route>();
            Response = _routeBusiness.Save(_Route);
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Convert.ToInt64(Response.ObjectResult);
        }

        // PUT api/route/5
        public bool Put([FromBody]Route _Route)
        {
            ServiceResponse<Route> Response = new ServiceResponse<Route>();
            Response = _routeBusiness.Update(_Route);
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;
        }

        // DELETE api/route/5
        public void Delete(int id)
        {
        }
    }
}

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
    public class CompanyController : ApiController
    {
        CompanyBusinessLayer _repository = new CompanyBusinessLayer();
        // GET api/company
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/company/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/company
        public long Post([FromBody]Company _company)
        {
            ServiceResponse<Company> Response = new ServiceResponse<Company>();
            Response=_repository.Save(_company);
            if(Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest,Response.Message));
            return Convert.ToInt64(Response.ObjectResult);
        }

        // PUT api/company/5
        public bool Put([FromBody]Company _company)
        {
            ServiceResponse<Company> Response = new ServiceResponse<Company>();
            Response = _repository.Update(_company);
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;
        }

        // DELETE api/company/5
        public void Delete(int id)
        {
        }
    }
}

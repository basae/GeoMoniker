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
    public class OnenessController : ApiController
    {
        OnenessBusinessLayer _Repository = new OnenessBusinessLayer();
        // GET api/oneness
        public IEnumerable<Oneness> Get(long IdCompany)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            Response = _Repository.GetByIdCompany(IdCompany);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.List;
        }

        // GET api/oneness/5
        public Oneness Get(long idOneness,long IdCompany)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            Response = _Repository.GetById(idOneness);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (Oneness)Response.ObjectResult;
        }

        // POST api/oneness
        public long Post([FromBody]Oneness _Oneness,long idUser)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            Response = _Repository.Save(_Oneness, idUser);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (long)Response.ObjectResult;
        }

        // PUT api/oneness/5
        public long Put([FromBody]Oneness _Oneness,long idUser)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            Response = _Repository.Update(_Oneness, idUser);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (long)Response.ObjectResult;
        }

        // DELETE api/oneness/5
        public void Delete(int id)
        {
        }
    }
}

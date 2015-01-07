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
    public class TurnController : ApiController
    {
        private TurnBusinessLayer _repository = new TurnBusinessLayer();
        // GET api/turn
        public IEnumerable<Turn> Get(long IdRoute)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            Response = _repository.GetByRouteId(IdRoute);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.List;
        }

        // GET api/turn/5
        public Turn Get(long IdRoute,long id)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            Response = _repository.GetById(id);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (Turn)Response.ObjectResult;
            
        }

        // POST api/turn
        public long Post([FromBody]Turn _turn)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            Response=_repository.Save(_turn);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (long)Response.ObjectResult;
        }

        // PUT api/turn/5
        public long Put([FromBody]Turn _turn)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            Response = _repository.Update(_turn);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (long)Response.ObjectResult;
        }

        // DELETE api/turn/5
        public void Delete(int id)
        {
        }
    }
}

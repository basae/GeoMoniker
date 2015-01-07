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
    public class TurnControlController : ApiController
    {
        private TurnControlBusinessLayer _repository = new TurnControlBusinessLayer();
        // GET api/turncontrol
        public IEnumerable<TurnControl> Get()
        {
            if (!Request.Headers.Contains("DateControl"))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se encontro un valor en la cabecera 'DateControl'"));
            DateTime datecontrol =DateTime.Parse(Request.Headers.GetValues("DateControl").FirstOrDefault());
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            Response = _repository.GetByDateControl(datecontrol);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));

            return Response.List;
        }

        // GET api/turncontrol/5
        public TurnControl Get(string OnenessId)
        {
            if (!Request.Headers.Contains("DateControl"))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se encontro un valor en la cabecera 'DateControl'"));
            DateTime datecontrol = DateTime.Parse(Request.Headers.GetValues("DateControl").FirstOrDefault());
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            Response = _repository.GetByOnenessId(datecontrol,OnenessId);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));

            return (TurnControl)Response.ObjectResult;
            
        }

        // POST api/turncontrol
        public long Post([FromBody]TurnControl _turnControl)
        {
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            Response = _repository.Save(_turnControl);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));

            return (long)Response.ObjectResult;
        }

        // PUT api/turncontrol/5
        public long Put([FromBody]TurnControl _turnControl)
        {
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            Response = _repository.Update(_turnControl);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));

            return (long)Response.ObjectResult;
        }

        // DELETE api/turncontrol/5
        public void Delete(int id)
        {
        }
    }
}

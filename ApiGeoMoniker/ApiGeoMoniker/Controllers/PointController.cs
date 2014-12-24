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
    public class PointController : ApiController
    {
        private PointBusinessLayer _pointRepository = new PointBusinessLayer();
        // GET api/point
        public IEnumerable<Point> Get(long IdRoute)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            Response = _pointRepository.SelectByRoute(IdRoute);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.List;
        }

        // GET api/point/5
        public Point Get(long IdRoute,long id)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            Response = _pointRepository.SelectById(id);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (Point)Response.ObjectResult;
        }

        // POST api/point
        public long Post([FromBody]Point _point,long IdRoute)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            Response = _pointRepository.Save(_point, IdRoute);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (long)Response.ObjectResult;
        }

        // PUT api/point/5
        public bool Put(long IdRoute, [FromBody]Point _point)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            Response = _pointRepository.Update(_point, IdRoute);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;
        }

        // DELETE api/point/5
        public void Delete(int id)
        {
        }
    }
}

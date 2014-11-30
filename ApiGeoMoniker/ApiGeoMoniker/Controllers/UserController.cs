using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using business;


namespace MonitorGps.Controllers
{
    public class UserController : ApiController
    {
        // GET api/user

        private UserBusinessLayer _userRepository = new UserBusinessLayer();

        public IEnumerable<User> Get()
        {
            ServiceResponse<User> Response = _userRepository.Select();
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.List;
        }

        // GET api/user/5
        public User Get(long id)
        {
            ServiceResponse<User> Response = _userRepository.Select(id);
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return (User)Response.ObjectResult;
        }

        // POST api/user
        public long Post([FromBody]User Usr)
        {
            ServiceResponse<User> Response = _userRepository.Save(Usr);
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Convert.ToInt64(Response.ObjectResult);
        }

        // PUT api/user/5
        public bool Put([FromBody]User Usr)
        {
            ServiceResponse<User> Response = _userRepository.Update(Usr);
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;
        }

        // DELETE api/user/5
        public bool Delete(long id)
        {
            ServiceResponse<User> Response = _userRepository.Delete(id);
            if (Response.Error)
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;
        }
    }
}

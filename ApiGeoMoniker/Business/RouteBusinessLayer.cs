using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Data;

namespace Business
{
    public class RouteBusinessLayer
    {
        private RouteRepository _repository;
        public RouteBusinessLayer()
        {
            _repository = new RouteRepository();
        }

        public ServiceResponse<Route> Save(Route _Route)
        {
            ServiceResponse<Route> Response = new ServiceResponse<Route>();
            try
            {
                if (_Route == null)
                    throw new Exception("Objeto invalido");

                if (_Route.Id.HasValue && _Route.Id != 0)
                    throw new Exception("El Id debe ser nulo o 0");
                if (string.IsNullOrWhiteSpace(_Route.Name))
                    throw new Exception("El nombre de la ruta es obligatorio");
                if (_Route.IdCompany == null || _Route.IdCompany<=0)
                    throw new Exception("Es necesario un Id de empresa valido");
                if (_Route.User == null || _Route.User <= 0)
                    throw new Exception("Es necesario un Id de Usuario Valido");
                Response=_repository.Save(_Route);
                if (Response.Error)
                    throw new Exception(Response.Message);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public ServiceResponse<Route> Update(Route _Route)
        {
            ServiceResponse<Route> Response = new ServiceResponse<Route>();
            try
            {
                if (_Route == null)
                    throw new Exception("Objeto invalido");

                if (!_Route.Id.HasValue || _Route.Id <= 0)
                    throw new Exception("El Id debe tener un valor valido");
                if (string.IsNullOrWhiteSpace(_Route.Name))
                    throw new Exception("El nombre de la ruta es obligatorio");
                if (_Route.User == null || _Route.User <= 0)
                    throw new Exception("Es necesario un Id de Usuario Valido");
                Response = _repository.Save(_Route);
                if (Response.Error)
                    throw new Exception(Response.Message);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public ServiceResponse<Route> SelectById(long Id)
        {
            ServiceResponse<Route> Response = new ServiceResponse<Route>();
            try
            {
                if (Id == null)
                    throw new Exception("Es necesario el ID de la ruta");

                Response = _repository.SelectById(Id);
                if (Response.Error)
                    throw new Exception(Response.Message);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public ServiceResponse<RouteOuput> SelectByCompany(long IdCompany)
        {
            ServiceResponse<RouteOuput> Response = new ServiceResponse<RouteOuput>();
            try
            {
                if (IdCompany == null)
                    throw new Exception("Es necesario el ID de la Compañia");

                Response = _repository.SelectByCompany(IdCompany);
                if (Response.Error)
                    throw new Exception(Response.Message);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }



    }
}

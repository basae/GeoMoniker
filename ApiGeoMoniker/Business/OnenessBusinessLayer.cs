using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Data;

namespace Business
{
    public class OnenessBusinessLayer
    {
        OnenessRepository _repository;
        public OnenessBusinessLayer()
        {
            _repository = new OnenessRepository();
        }

        public ServiceResponse<Oneness> Save(Oneness _oneness, long IdUser)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            try
            {
                if (_oneness == null)
                    throw new Exception("El Objeto 'Unidad' esta vacio o error de formato");
                if (IdUser == null || IdUser == 0)
                    throw new Exception("Id de Usuario Valido Es necesario");
                if (_oneness.IdCompany == null || _oneness.IdCompany == 0)
                    throw new Exception("Id de Compañia Invalido o Vacio");
                if (_oneness.Id != null && _oneness.Id != 0)
                    throw new Exception("Id incorrecto debe ser nulo o 0 para un nuevo registro");
                if (string.IsNullOrWhiteSpace(_oneness.Name))
                    throw new Exception("El Nombre de la Unidad es Requerido");

                Response = _repository.Save(_oneness, IdUser);
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

        public ServiceResponse<Oneness> Update(Oneness _oneness, long IdUser)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            try
            {
                if (_oneness == null)
                    throw new Exception("El Objeto 'Unidad' esta vacio o error de formato");
                if (IdUser == null || IdUser == 0)
                    throw new Exception("Id de Usuario Valido Es necesario");
                if (_oneness.IdCompany == null || _oneness.IdCompany != 0)
                    throw new Exception("Id de Compañia Invalido o Vacio");
                if (_oneness.Id == null || _oneness.Id == 0)
                    throw new Exception("Id incorrecto debe ser nulo o 0 para un nuevo registro");
                if (string.IsNullOrWhiteSpace(_oneness.Name))
                    throw new Exception("El Nombre de la Unidad es Requerido");

                Response = _repository.Save(_oneness, IdUser);
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


        public ServiceResponse<Oneness> GetById(long IdOneness)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            try
            {
                if (IdOneness == null || IdOneness==0)
                    throw new Exception("Es Necesario un Id Valido");
                Response = _repository.GetById(IdOneness);
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

        public ServiceResponse<Oneness> GetByIdCompany(long IdCompany)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            try
            {
                if (IdCompany == null || IdCompany == 0)
                    throw new Exception("Es Necesario un Id Valido");
                Response = _repository.GetByIdCompany(IdCompany);
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

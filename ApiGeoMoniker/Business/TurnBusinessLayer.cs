using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Data;


namespace Business
{
    public class TurnBusinessLayer
    {
        TurnRepository _repository;
        public TurnBusinessLayer()
        {
            _repository = new TurnRepository();
        }

        public ServiceResponse<Turn> Save(Turn _turn)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            try
            {
                if (_turn == null)
                    throw new Exception("Objeto Vacio o formato incorrecto");
                if (_turn.Id != null && _turn.Id!=0)
                    throw new Exception("Id incorrecto, debe ser nulo o 0");
                if (_turn.IdRoute == null || _turn.IdRoute<=0)
                    throw new Exception("Id de Ruta Invalido");
                if (_turn.IdPoint == null || _turn.IdPoint<=0)
                    throw new Exception("Id de Terminal Invalido");
                if (_turn.AwaitedArrival == null)
                    throw new Exception("Es necesario establecer una fecha valida");
                Response = _repository.Save(_turn);
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

        public ServiceResponse<Turn> Update(Turn _turn)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            try
            {
                if (_turn == null)
                    throw new Exception("Objeto Vacio o formato incorrecto");
                if (_turn.Id == null || _turn.Id == 0)
                    throw new Exception("Id incorrecto, no puede ser nulo o 0");
                if (_turn.IdRoute == null || _turn.IdRoute <= 0)
                    throw new Exception("Id de Ruta Invalido");
                if (_turn.IdPoint == null || _turn.IdPoint<=0)
                    throw new Exception("Id de Terminal Invalido");
                if (_turn.AwaitedArrival == null)
                    throw new Exception("Es necesario establecer una fecha valida");
                Response = _repository.Save(_turn);
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

        public ServiceResponse<Turn> GetById(long TurnId)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            try
            {
                if (TurnId == null)
                    throw new Exception("Id Incorrecto, no puede estar vacio");
                Response = _repository.GetById(TurnId);
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

        public ServiceResponse<Turn> GetByRouteId(long RouteId)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            try
            {
                if (RouteId == null)
                    throw new Exception("Id Incorrecto, no puede estar vacio");
                Response = _repository.GetByRouteId(RouteId);
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

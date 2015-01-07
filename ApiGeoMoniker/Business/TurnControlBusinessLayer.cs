using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Models;

namespace Business
{
    public class TurnControlBusinessLayer
    {
        private TurnControlRepository _repsitory;
        public TurnControlBusinessLayer()
        {
            _repsitory = new TurnControlRepository();
        }

        public ServiceResponse<TurnControl> Save(TurnControl _turnControl)
        {
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            try
            {
                if (_turnControl == null)
                    throw new Exception("Objeto Vacio o error en formato");
                if (_turnControl.Id != null && _turnControl.Id != 0)
                    throw new Exception("El Id debe ser nulo o 0");
                if (_turnControl.IdTurn == null || _turnControl.IdTurn<=0)
                    throw new Exception("Id de Vuelta Invalido");
                if (_turnControl.IdOneness == null || _turnControl.IdOneness <= 0)
                    throw new Exception("Id de Unidad invalido");
                if (_turnControl.DateControl == null)
                    throw new Exception("Fecha de control requerida");

                Response = _repsitory.Save(_turnControl);
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

        public ServiceResponse<TurnControl> Update(TurnControl _turnControl)
        {
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            try
            {
                if (_turnControl == null)
                    throw new Exception("Objeto Vacio o error en formato");
                if (_turnControl.Id == null || _turnControl.Id <= 0)
                    throw new Exception("El Id no debe estar vacio y ser mayor a 0");
                if (_turnControl.IdTurn == null || _turnControl.IdTurn <= 0)
                    throw new Exception("Id de Vuelta Invalido");
                if (_turnControl.IdOneness == null || _turnControl.IdOneness <= 0)
                    throw new Exception("Id de Unidad invalido");
                if (_turnControl.DateControl == null)
                    throw new Exception("Fecha de control requerida");

                Response = _repsitory.Save(_turnControl);
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

        public ServiceResponse<TurnControl> GetByDateControl(DateTime DateControl)
        {
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            try
            {
                if (DateControl == null)
                    throw new Exception("Fecha de control requerida");

                Response = _repsitory.GetByDate(DateControl);
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

        public ServiceResponse<TurnControl> GetByOnenessId(DateTime DateControl,string OnenessId)
        {
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            try
            {
                if (DateControl == null)
                    throw new Exception("Fecha de control requerida");

                if (string.IsNullOrWhiteSpace(OnenessId))
                    throw new Exception("Nombre de la Unidad Requerida");

                Response = _repsitory.GetByOnenessName(OnenessId, DateControl);
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

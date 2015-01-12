using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Models;

namespace Business
{
    public class ArrivedControlBusinessLayer
    {
        ArrivedControlRepository _repository;
        public ArrivedControlBusinessLayer()
        {
            _repository = new ArrivedControlRepository();
        }

        public ServiceResponse<ArrivedControl> GetByDateControl(DateTime DateControl)
        {
            ServiceResponse<ArrivedControl> Response = new ServiceResponse<ArrivedControl>();
            try
            {
                if (DateControl == null)
                    throw new Exception("Es necesaria la fecha de verificación");
                if (DateControl == DateTime.MaxValue || DateControl==DateTime.MinValue)
                    throw new Exception("Fecha incorrecta");

                Response=_repository.GetByDateControl(DateControl);
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

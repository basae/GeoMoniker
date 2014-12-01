﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Data;

namespace Business
{
    public class PointBusinessLayer
    {
        private PointsRepository _pointRepository;
        public PointBusinessLayer()
        {
            _pointRepository = new PointsRepository();
        }

        public ServiceResponse<Point> Save(Point _point, long IdRoute)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            try
            {
                if (_point == null)
                    throw new Exception("El objeto Point esta vacio");
                if (_point.Id.HasValue && _point.Id != 0)
                    throw new Exception("El Id de la terminal debe ser nulo o 0 para un nuevo registro");
                if (string.IsNullOrWhiteSpace(_point.Description))
                    throw new Exception("Es necesaria la descripcion de la terminal");
                if (_point.Description.Length > 100)
                    throw new Exception("La descripcion no debe ser mayor a 100 caracteres");
                if (_point.Lat == null)
                    throw new Exception("Latitud Requerida");
                if (_point.Lng == null)
                    throw new Exception("Longitud requerida");
                if(IdRoute==null || IdRoute<=0)
                    throw new Exception("El Id de la ruta debe ser valido");
                Response=_pointRepository.Save(_point,IdRoute);
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

        public ServiceResponse<Point> Update(Point _point, long IdRoute)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            try
            {
                if (_point == null)
                    throw new Exception("El objeto Point esta vacio");
                if (!_point.Id.HasValue || _point.Id <= 0)
                    throw new Exception("El id debe tener un valor valido");
                if (string.IsNullOrWhiteSpace(_point.Description))
                    throw new Exception("Es necesaria la descripcion de la terminal");
                if (_point.Description.Length > 100)
                    throw new Exception("La descripcion no debe ser mayor a 100 caracteres");
                if (_point.Lat == null)
                    throw new Exception("Latitud Requerida");
                if (_point.Lng == null)
                    throw new Exception("Longitud requerida");
                if (IdRoute == null || IdRoute <= 0)
                    throw new Exception("El Id de la ruta debe ser valido");
                Response = _pointRepository.Save(_point, IdRoute);
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

        public ServiceResponse<Point> SelectByRoute(long IdRoute)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            try
            {
                if (IdRoute == null || IdRoute <= 0)
                    throw new Exception("El Id de la ruta debe ser valido");
                Response = _pointRepository.SelectByRoute(IdRoute);
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

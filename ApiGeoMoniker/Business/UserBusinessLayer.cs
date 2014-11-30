using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Models;

namespace business
{
    public class UserBusinessLayer
    {
        private UserRepository _userRepository;
        public UserBusinessLayer()
        {
            _userRepository = new UserRepository();
        }

        public ServiceResponse<User> Save(User Usr)
        {
            ServiceResponse<User> Response = new ServiceResponse<User>();
            try
            {
                if (Usr == null)
                    throw new Exception("El Objeto Usuario no deber ser Nulo");
                if (Usr.Id.HasValue && Usr.Id != 0)
                    throw new Exception("El valor de Id para un registro nuevo debe ser nulo o 0");
                if (string.IsNullOrEmpty(Usr.FirstName))
                    throw new Exception("Campo 'FirstName' es Obligatorio");
                if (string.IsNullOrEmpty(Usr.LastName))
                    throw new Exception("Campo 'LastName' es Obligatorio");
                if (!Usr.Type.HasValue || Usr.Type <= 0)
                    throw new Exception("Campo 'Type' Debe ser un Valor Valido");
                if (!Usr.IdCompany.HasValue)
                    throw new Exception("Es necesario el ID de la Empresa");
                Response = _userRepository.Save(Usr);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public ServiceResponse<User> Update(User Usr)
        {
            ServiceResponse<User> Response = new ServiceResponse<User>();
            try
            {
                if (Usr == null)
                    throw new Exception("El Objeto Usuario no deber ser Nulo");
                if (!Usr.Id.HasValue || Usr.Id <= 0)
                    throw new Exception("Debe Contener Id Valido");
                if (string.IsNullOrEmpty(Usr.FirstName))
                    throw new Exception("Campo 'FirstName' es Obligatorio");
                if (string.IsNullOrEmpty(Usr.LastName))
                    throw new Exception("Campo 'LastName' es Obligatorio");
                if (!Usr.Type.HasValue || Usr.Type <= 0)
                    throw new Exception("Campo 'Type' Debe ser un Valor Valido");
                Response = _userRepository.Save(Usr);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public ServiceResponse<User> Delete(long IdUser)
        {
            ServiceResponse<User> Response = new ServiceResponse<User>();
            try
            {
                if (IdUser == null)
                    throw new Exception("Es Necesario el ID");
                if (IdUser <=0 )
                    throw new Exception("Debe Contener Id Valido");
                Response = _userRepository.Delete(IdUser);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public ServiceResponse<User> Select(long IdUser)
        {
            ServiceResponse<User> Response = new ServiceResponse<User>();
            try
            {
                if (IdUser == null)
                    throw new Exception("Es Necesario el ID");
                if (IdUser <= 0)
                    throw new Exception("Debe Contener Id Valido");
                Response = _userRepository.Select(IdUser);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public ServiceResponse<User> Select()
        {
            ServiceResponse<User> Response = new ServiceResponse<User>();
            try
            {
                Response = _userRepository.Select();
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

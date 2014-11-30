using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Models;

namespace Business
{
    public class CompanyBusinessLayer
    {
        private CompanyRespository _companyRepository;

        public CompanyBusinessLayer()
        {
            _companyRepository = new CompanyRespository();
        }

        public ServiceResponse<Company> Save(Company _Company)
        {
            ServiceResponse<Company> Response = new ServiceResponse<Company>();
            try
            {
                if (_Company == null)
                    throw new Exception("El Objeto Empresa no debe estar vacio");
                if (_Company.Id.HasValue && _Company.Id != 0)
                    throw new Exception("El Id debe ser nulo o 0 para un nuevo registro");
                if (string.IsNullOrWhiteSpace(_Company.Name))
                    throw new Exception("Es necesario el Nombre de la Empresa");
                Response=_companyRepository.Save(_Company);
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

        public ServiceResponse<Company> Update(Company _Company)
        {
            ServiceResponse<Company> Response = new ServiceResponse<Company>();
            try
            {
                if (_Company == null)
                    throw new Exception("El Objeto Empresa no debe estar vacio");
                if (!_Company.Id.HasValue || _Company.Id <= 0)
                    throw new Exception("El Id debe ser contener un valor valido");
                if (string.IsNullOrWhiteSpace(_Company.Name))
                    throw new Exception("Es necesario el Nombre de la Empresa");
                Response = _companyRepository.Save(_Company);
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

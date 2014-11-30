using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.Data;

namespace Data
{
    public class CompanyRespository:BaseRepository
    {
        public ServiceResponse<Company> Save(Company _company)
        {
            ServiceResponse<Company> Response = new ServiceResponse<Company>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_IU_Company", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.BigInt, 64).Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["@Id"].Value = _company.Id;
                        cmd.Parameters.AddWithValue("@Name", _company.Name);
                        cmd.ExecuteNonQuery();
                        
                        if (Convert.ToInt64(cmd.Parameters["@Id"].Value)!=_company.Id)
                        {
                            Response.ObjectResult = cmd.Parameters["@Id"].Value;
                        }
                        else
                        {
                            throw new Exception("No Se encontro el Registro en la BD");
                        }
                    }
                }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Models;
using Data.Core;

namespace Data
{
    public class RouteRepository:BaseRepository
    {
        public ServiceResponse<Route> Save(Route route)
        {
            ServiceResponse<Route> Response = new ServiceResponse<Route>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_IU_Route", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.BigInt, 64).Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["@Id"].Value = route.Id;
                        cmd.Parameters.AddWithValue("@Name",route.Name);
                        cmd.Parameters.AddWithValue("@UserInsert", route.User);
                        cmd.Parameters.AddWithValue("@IdCompany", route.IdCompany);
                        cmd.ExecuteNonQuery();
                        if((Convert.ToInt64(cmd.Parameters["@Id"].Value)!=route.Id))
                            Response.ObjectResult=cmd.Parameters["@Id"].Value;
                        else
                            throw new Exception("No Se Econtro en la BD la Ruta");

                    }
                }
            }
            catch(Exception ex)
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
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_RouteByCompany", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCompany", IdCompany);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        //new Utilities().QuitNullValueOfTable(ref result);
                        if (result.Rows.Count > 0)
                        {
                            Response.List = from row in result.Rows.Cast<DataRow>()
                                            select new RouteOuput
                                            {
                                                Id = (row["ID"] != DBNull.Value) ? (long)row["ID"] : (long?)null,
                                                Name = (row["NAME"] != DBNull.Value) ? (string)row["NAME"] : "",
                                                Company = (row["COMPANY"] != DBNull.Value) ? (string)row["COMPANY"] : "",
                                                UserInsert = (row["INSERTUSER"] != DBNull.Value) ? (string)row["INSERTUSER"] : "",
                                                InsertDate = (row["INSERTDATE"] != DBNull.Value) ? (DateTime)row["INSERTDATE"] : (DateTime?)null,
                                                UserUpdate = (row["UPDATEUSER"] != DBNull.Value) ? (string)row["UPDATEUSER"] : "",
                                                UpdateDate = (row["UPDATEDATE"] != DBNull.Value) ? (DateTime)row["UPDATEDATE"] : (DateTime?)null
                                            };

                            if(Response.List.OfType<Exception>().Count()>0)
                                throw new Exception(Response.List.OfType<Exception>().FirstOrDefault().Message);
                        }
                        else
                            throw new Exception("No Se Econtro en la BD la Ruta");

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

        public ServiceResponse<Route> SelectById(long Id)
        {
            ServiceResponse<Route> Response = new ServiceResponse<Route>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_RouteById", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        if (result.Rows.Count > 0)
                        {
                            Response.ObjectResult = (from row in result.Rows.Cast<DataRow>()
                                            select new RouteOuput
                                            {
                                                Id = (row["ID"]!=DBNull.Value)?(long)row["ID"]:(long?)null,
                                                Name = (row["NAME"]!=DBNull.Value)?(string)row["NAME"]:"",
                                                Company=(row["COMPANY"]!=DBNull.Value)?(string)row["COMPANY"]:"",
                                                UserInsert =(row["INSERTUSER"]!=DBNull.Value)?(string)row["INSERTUSER"]:"",
                                                InsertDate=(row["INSERTDATE"]!=DBNull.Value)?(DateTime)row["INSERTDATE"]:(DateTime?)null,
                                                UserUpdate=(row["UPDATEUSER"]!=DBNull.Value)?(string)row["UPDATEUSER"]:"",
                                                UpdateDate=(row["UPDATEDATE"]!=DBNull.Value)?(DateTime)row["UPDATEDATE"]:(DateTime?)null
                                            }).FirstOrDefault();

                            if (Response.ObjectResult==null)
                                throw new Exception(Response.List.OfType<Exception>().FirstOrDefault().Message);
                        }
                        else
                            throw new Exception("No Se Econtro en la BD la Ruta");

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

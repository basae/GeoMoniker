using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.Data;
using Data.Core;

namespace Data
{
    public class OnenessRepository:BaseRepository
    {
        public ServiceResponse<Oneness> Save(Oneness _oneness, long IdUser)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_IU_Oneness", Conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt, 64).Direction = System.Data.ParameterDirection.InputOutput;
                        cmd.Parameters["@Id"].Value = _oneness.Id;
                        cmd.Parameters.AddWithValue("@Name", _oneness.Name);
                        cmd.Parameters.AddWithValue("@Owner", _oneness.Owner);
                        cmd.Parameters.AddWithValue("@IdCompany", _oneness.IdCompany);
                        cmd.Parameters.AddWithValue("@IdUser", IdUser);
                        cmd.Parameters.AddWithValue("@Lat", _oneness.Lat);
                        cmd.Parameters.AddWithValue("@Lng", _oneness.Lng);
                        cmd.ExecuteNonQuery();
                        Response.ObjectResult = cmd.Parameters["@Id"].Value;

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

        public ServiceResponse<Oneness> GetById(long IdOneness)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_OnenessById", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", IdOneness);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        Response.ObjectResult = (from row in dt.Rows.Cast<DataRow>()
                                                 select new Oneness
                                                 {
                                                     Id=(long)row["Id"],
                                                     Name=(string)row["Name"],
                                                     Owner = (row["Owner"] == DBNull.Value) ? null : (string)row["Owner"],
                                                     InsertDate=(row["InsertDate"]==DBNull.Value)?null:(DateTime?)row["InsertDate"],
                                                     UpdateDate=(row["UpdateDate"]==DBNull.Value)?null:(DateTime?)row["UpdateDate"],
                                                     IdCompany=(long)row["IdCompany"],
                                                     Lat=(row["Lat"]==DBNull.Value)?null:(decimal?)row["Lat"],
                                                     Lng=(row["Lng"]==DBNull.Value)?null:(decimal?)row["Lng"]
                                                 }).FirstOrDefault();

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

        public ServiceResponse<Oneness> GetByIdCompany(long IdCompany)
        {
            ServiceResponse<Oneness> Response = new ServiceResponse<Oneness>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_OnenessByIdCompany", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCompany", IdCompany);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Response.List = from row in dt.Rows.Cast<DataRow>()
                                        select new Oneness
                                        {
                                            Id = (long)row["Id"],
                                            Name = (string)row["Name"],
                                            Owner = (row["Owner"] == DBNull.Value) ? null : (string)row["Owner"],
                                            InsertDate = (row["InsertDate"] == DBNull.Value) ? null : (DateTime?)row["InsertDate"],
                                            UpdateDate = (row["UpdateDate"] == DBNull.Value) ? null : (DateTime?)row["UpdateDate"],
                                            IdCompany = (long)row["IdCompany"],
                                            Lat = (row["Lat"] == DBNull.Value) ? null : (decimal?)row["Lat"],
                                            Lng = (row["Lng"] == DBNull.Value) ? null : (decimal?)row["Lng"]
                                        };
                        if (Response.List.OfType<Exception>().Count() > 0)
                            throw new Exception(Response.List.OfType<Exception>().FirstOrDefault().Message);

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

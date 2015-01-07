using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class TurnRepository:BaseRepository
    {
        public ServiceResponse<Turn> Save(Turn turn)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_IU_Turn", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Id", SqlDbType.BigInt).Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["@Id"].Value = turn.Id;

                        cmd.Parameters.AddWithValue("@IdRoute", turn.IdRoute);
                        cmd.Parameters.AddWithValue("@IdPoint", turn.IdPoint);
                        cmd.Parameters.AddWithValue("@AwaitedArrival", turn.AwaitedArrival);

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

        public ServiceResponse<Turn> GetByRouteId(long RouteId)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_TurnByRouteId", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRoute", RouteId);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        Response.List = from row in dt.Rows.Cast<DataRow>()
                                        select new Turn
                                        {
                                            Id = (long)row["Id"],
                                            IdRoute = (long)row["IdRoute"],
                                            IdPoint = (long)row["IdPoint"],
                                            AwaitedArrival = (DateTime)row["AwaitedArrival"]
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

        public ServiceResponse<Turn> GetById(long TurnId)
        {
            ServiceResponse<Turn> Response = new ServiceResponse<Turn>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_TurnById", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", TurnId);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        Response.ObjectResult = (from row in dt.Rows.Cast<DataRow>()
                                                 select new Turn
                                                 {
                                                     Id = (long)row["Id"],
                                                     IdRoute = (long)row["IdRoute"],
                                                     IdPoint = (long)row["IdPoint"],
                                                     AwaitedArrival = (DateTime)row["AwaitedArrival"]
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

    }
}

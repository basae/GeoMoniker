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
    public class PointsRepository:BaseRepository
    {
        public ServiceResponse<Point> Save(Point _Point,long IdRoute)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                        using(SqlCommand cmd=new SqlCommand("SP_IU_Point",Conn))
                        {
                            cmd.CommandType= CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Id", SqlDbType.BigInt, 64).Direction = ParameterDirection.InputOutput;
                            cmd.Parameters["@Id"].Value = _Point.Id;
                            cmd.Parameters.AddWithValue("@Description", _Point.Description);
                            cmd.Parameters.AddWithValue("@Lat", _Point.Lat);
                            cmd.Parameters.AddWithValue("@Lng", _Point.Lng);
                            cmd.Parameters.AddWithValue("@IdRute", IdRoute);
                            cmd.Parameters.AddWithValue("@IsStart", _Point.IsStart);
                            cmd.Parameters.AddWithValue("@IsEnd", _Point.isEnd);
                            cmd.Parameters.AddWithValue("@OrderRoute", _Point.Order);
                            cmd.Parameters.AddWithValue("@LatAreaMax", _Point.LatAreaMax);
                            cmd.Parameters.AddWithValue("@LatAreaMin", _Point.LatAreaMin);
                            cmd.Parameters.AddWithValue("@LngAreaMax", _Point.LngAreaMax);
                            cmd.Parameters.AddWithValue("@LngAreaMin", _Point.LngAreaMin);
                            
                            cmd.ExecuteNonQuery();

                            if (Convert.ToInt64(cmd.Parameters["@Id"].Value) != _Point.Id)
                            {
                                Response.ObjectResult = cmd.Parameters["@Id"].Value;
                            }
                            else
                                throw new Exception("No Se encontro la Terminal en la BD");
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

        public ServiceResponse<Point> SelectByRoute(long IdRoute)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_PointByRoute", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRute", IdRoute);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        if (result.Rows.Count>0)
                        {
                            Response.List = (from row in result.Rows.Cast<DataRow>()
                                            select new Point
                                            {
                                                Id=(long)row["Id"],
                                                Description=(string)row["Description"],
                                                Lat=(decimal)row["Lat"],
                                                Lng=(decimal)row["Lng"],
                                                IsStart=(bool)row["IsStart"],
                                                isEnd=(bool)row["IsEnd"],
                                                Order = (row["OrderRoute"] == DBNull.Value) ? 0 : (int)row["OrderRoute"],
                                                LatAreaMax=(row["LatAreaMax"]==DBNull.Value)?(decimal?)null:(decimal)row["LatAreaMax"],
                                                LatAreaMin = (row["LatAreaMin"] == DBNull.Value) ? (decimal?)null : (decimal)row["LatAreaMin"],
                                                LngAreaMin = (row["LngAreaMin"] == DBNull.Value) ? (decimal?)null : (decimal)row["LngAreaMin"],
                                                LngAreaMax = (row["LngAreaMax"] == DBNull.Value) ? (decimal?)null : (decimal)row["LngAreaMax"]
                                            }).OrderBy(x => x.Order);
                        }
                        else
                            throw new Exception("No Se encontro la Ruta en la BD");
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

        public ServiceResponse<Point> SelectById(long IdPoint)
        {
            ServiceResponse<Point> Response = new ServiceResponse<Point>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_PointByID", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPoint", IdPoint);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        if (result.Rows.Count>0)
                        {
                            Response.ObjectResult = (from row in result.Rows.Cast<DataRow>()
                                            select new Point
                                            {
                                                Id=(long)row["Id"],
                                                Description=(string)row["Description"],
                                                Lat=(decimal)row["Lat"],
                                                Lng=(decimal)row["Lng"],
                                                IsStart=(bool)row["IsStart"],
                                                isEnd=(bool)row["IsEnd"],
                                                Order = (row["OrderRoute"] == DBNull.Value) ? 0 : (int)row["OrderRoute"],
                                                LatAreaMax = (row["LatAreaMax"] == DBNull.Value) ? (decimal?)null : (decimal)row["LatAreaMax"],
                                                LatAreaMin = (row["LatAreaMin"] == DBNull.Value) ? (decimal?)null : (decimal)row["LatAreaMin"],
                                                LngAreaMin = (row["LngAreaMin"] == DBNull.Value) ? (decimal?)null : (decimal)row["LngAreaMin"],
                                                LngAreaMax = (row["LngAreaMax"] == DBNull.Value) ? (decimal?)null : (decimal)row["LngAreaMax"]
                                            }).FirstOrDefault();
                        }
                        else
                            throw new Exception("No Se encontro la Ruta en la BD");
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

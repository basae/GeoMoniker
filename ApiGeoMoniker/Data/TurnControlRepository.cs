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
    public class TurnControlRepository:BaseRepository
    {
        public ServiceResponse<TurnControl> Save(TurnControl _turnControl)
        {
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_IU_TurnControl", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Id", SqlDbType.BigInt).Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["@Id"].Value = _turnControl.Id;
                        cmd.Parameters.AddWithValue("@DateControl", _turnControl.DateControl);
                        cmd.Parameters.AddWithValue("@IdTurn", _turnControl.IdTurn);
                        cmd.Parameters.AddWithValue("@IdOneness", _turnControl.IdOneness);
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

        public ServiceResponse<TurnControl> GetByDate(DateTime DateControl)
        {
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_TurnControlByDate", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DateControl", DateControl);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Response.List = from row in dt.Rows.Cast<DataRow>()
                                        select new TurnControl
                                        {
                                            Id=(long)row["Id"],
                                            IdOneness=(long)row["IdOneness"],
                                            IdTurn=(long)row["IdTurn"],
                                            DateControl=(DateTime)row["DateControl"]

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

        public ServiceResponse<TurnControl> GetByOnenessName(string Name,DateTime DateControl)
        {
            ServiceResponse<TurnControl> Response = new ServiceResponse<TurnControl>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_TurnControlByOnenessName", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OnenessName", Name);
                        cmd.Parameters.AddWithValue("@DateControl", DateControl);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Response.ObjectResult = (from row in dt.Rows.Cast<DataRow>()
                                                select new TurnControl
                                                {
                                                    Id = (long)row["Id"],
                                                    IdOneness = (long)row["IdOneness"],
                                                    IdTurn = (long)row["IdTurn"],
                                                    DateControl = (DateTime)row["DateControl"]

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

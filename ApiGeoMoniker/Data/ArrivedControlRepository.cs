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
    public class ArrivedControlRepository:BaseRepository
    {
        public ServiceResponse<ArrivedControl> GetByDateControl(DateTime datecontrol)
        {
            ServiceResponse<ArrivedControl> Response = new ServiceResponse<ArrivedControl>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_ArrivedControlByDate", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DateControl", datecontrol);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Response.List = from row in dt.Rows.Cast<DataRow>()
                                            select new ArrivedControl
                                            {
                                              Id=(long)row["id"],
                                              Unit=(string)row["Unit"],
                                              Terminal=(string)row["Terminal"],
                                              AwaitedArrival=row["awaitedarrival"].ToString(),
                                              ActualArrival=row["actualarrival"].ToString(),
                                              DiffMinutes=(int)row["difminutes"]                                           
                                            };
                            if (Response.List.OfType<Exception>().Count() > 0)
                                throw new Exception(Response.List.OfType<Exception>().FirstOrDefault().Message);
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

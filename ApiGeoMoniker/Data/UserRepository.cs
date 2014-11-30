using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Data.SqlClient;
using System.Data;
using Data.Core;


namespace Data
{
    public class UserRepository:BaseRepository
    {
        public ServiceResponse<User> Save(User usr)
        {
            ServiceResponse<User> Response = new ServiceResponse<User>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_IU_User", Conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.BigInt, 64).Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["@Id"].Value = usr.Id;
                        //cmd.Parameters.AddWithValue("@Id", usr.Id).Direction= ParameterDirection.InputOutput;
                        cmd.Parameters.AddWithValue("@FirstName", usr.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", usr.LastName);
                        cmd.Parameters.AddWithValue("@Celphone", usr.Celphone);
                        cmd.Parameters.AddWithValue("@Email", usr.Email);
                        cmd.Parameters.AddWithValue("@UserTypeId", (Int64)usr.Type);
                        cmd.Parameters.AddWithValue("@IdCompany", (Int64)usr.IdCompany);
                        cmd.Parameters.AddWithValue("@Street", usr.Address.Street);
                        cmd.Parameters.AddWithValue("@Number", usr.Address.Number);
                        cmd.Parameters.AddWithValue("@Cp", usr.Address.CP);
                        cmd.Parameters.AddWithValue("@Neighborhood", usr.Address.Neighborhood);
                        cmd.Parameters.AddWithValue("@State", usr.Address.State);
                        cmd.Parameters.AddWithValue("@Country", usr.Address.Country);
                        cmd.ExecuteNonQuery();
                        if (Convert.ToInt64(cmd.Parameters["@Id"].Value) != usr.Id)
                            Response.ObjectResult = cmd.Parameters["@Id"].Value;
                        else
                            throw new Exception("No se Encontro el Usuario En la BD");
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

        public ServiceResponse<User> Delete(long idUser)
        {
            ServiceResponse<User> Response = new ServiceResponse<User>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_D_User", Conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idUser);
                        cmd.ExecuteNonQuery();
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

        public ServiceResponse<User> Select(long idUser)
        {
            ServiceResponse<User> Response = new ServiceResponse<User>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_UserById", Conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idUser);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        new Utilities().QuitNullValueOfTable(ref result);

                        if (result.Rows.Count > 0)
                        {
                            Response.ObjectResult = new User()
                            {
                                Id = (long)result.Rows[0]["ID"],
                                FirstName = (string)result.Rows[0]["FIRSTNAME"],
                                LastName = (string)result.Rows[0]["LASTNAME"],
                                Celphone = (string)result.Rows[0]["CELPHONE"],
                                Email =(string)result.Rows[0]["EMAIL"],
                                Type = (UsertType)((long)result.Rows[0]["USERTYPEID"]),
                                IdCompany=(long)result.Rows[0]["IDCOMPANY"],
                                Address = new Address()
                                {
                                    Street = (string)result.Rows[0]["STREET"],
                                    Number = (string)result.Rows[0]["NUMBER"],
                                    CP = (string)result.Rows[0]["CP"],
                                    Neighborhood = (string)result.Rows[0]["NEIGHBORHOOD"],
                                    State = (string)result.Rows[0]["STATE"],
                                    Country = (string)result.Rows[0]["COUNTRY"]
                                }

                            };
                        }
                        else
                        {
                            throw new Exception("El usuario con el Id: " + idUser + " no fue encontrado en el sistema");
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

        public ServiceResponse<User> Select()
        {
            ServiceResponse<User> Response = new ServiceResponse<User>();
            try
            {
                using (Conn)
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_User", Conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        new Utilities().QuitNullValueOfTable(ref result);
                        if (result.Rows.Count > 0)
                        {
                            Response.List = from row in result.Rows.Cast<DataRow>()
                            select new User()
                            {
                                Id = (long)row["ID"],
                                FirstName = (string)row["FIRSTNAME"],
                                LastName = (string)row["LASTNAME"],
                                Celphone = (string)row["CELPHONE"],
                                Email = (string)row["EMAIL"],
                                Type = (UsertType)((long)row["USERTYPEID"]),
                                IdCompany=(long)row["IDCOMPANY"],
                                Address = new Address()
                                {
                                    Street = (string)row["STREET"],
                                    Number = (string)row["NUMBER"],
                                    CP = (string)row["CP"],
                                    Neighborhood = (string)row["NEIGHBORHOOD"],
                                    State = (string)row["STATE"],
                                    Country = (string)row["COUNTRY"]
                                }

                            };
                            if (Response.List.OfType<Exception>().Count() > 0)
                                throw new Exception(Response.List.OfType<Exception>().FirstOrDefault().Message);
                        }
                        else
                        {
                            throw new Exception("No se encontraron registros en la BD");
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

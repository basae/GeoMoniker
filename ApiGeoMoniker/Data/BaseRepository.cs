using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Data
{
    public class BaseRepository
    {
        public SqlConnection Conn;
        public BaseRepository()
        {
            Conn = new SqlConnection(ConfigurationManager.AppSettings["GeoMonikerBD"]);
        }
    }
}

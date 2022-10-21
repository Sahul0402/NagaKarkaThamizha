using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KarkaThamizha.Repository.SQLObject
{
    public class ConnectionManager
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionKARKA2017"].ConnectionString);
        }

    }
}
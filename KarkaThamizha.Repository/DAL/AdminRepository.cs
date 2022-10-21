using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static KarkaThamizha.Object.Models.MasterModels;

namespace KarkaThamizha.Repository.DAL
{
    public class AdminRepository
    {
        //public List<LoginModels> GetAllAdmin()
        //{
        //    using (SqlConnection con = ConnectionManager.GetConnection())
        //    {
        //        using (SqlCommand cmd = new SqlCommand("dbo.USP_GetAdminList", con))
        //        {
        //            List<LoginModels> lstLogin = new List<LoginModels>();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            con.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //            {
        //                while (reader.Read())
        //                {
        //                    lstLogin.Add(new LoginModels()
        //                    {
        //                        LoginID = Convert.ToInt16(reader["AdminUserID"]),
        //                        Name = Convert.ToString(reader["Name"]),
        //                    });
        //                }
        //            }
        //            return lstLogin;
        //        }
        //    }
        //}
    }
}
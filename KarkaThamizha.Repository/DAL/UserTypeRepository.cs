using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KarkaThamizha.Repository.DAL
{
    public class UserTypeRepository
    {
        public List<UserTypeModels> GetAllUserType()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllUserType, con))
                {
                    List<UserTypeModels> lstUserType = new List<UserTypeModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstUserType.Add(new UserTypeModels()
                            {
                                UserTypeID = Convert.ToByte(reader["UserTypeID"]),
                                UserType = Convert.ToString(reader["UserType"]),
                            });
                        }
                    }
                    return lstUserType;
                }
            }
        }
    }
}
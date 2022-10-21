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
    public class EventsTypeRepository
    {
        public List<EventsTypeModels> GetAllEventType()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("dbo.USP_GetAllEventType", con))
                {
                    List<EventsTypeModels> lstEventType = new List<EventsTypeModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstEventType.Add(new EventsTypeModels()
                            {
                                EventsTypeID = Convert.ToByte(reader["EventsTypeID"]),
                                EventsType = Convert.ToString(reader["EventsType"]),
                            });
                        }
                    }
                    return lstEventType;
                }
            }
        }
    }
}
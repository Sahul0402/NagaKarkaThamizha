using KarkaThamizha.Common.Utility;
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
    public class EventsRepository
    {
        public List<EventsModels> GetAllEventsDetails()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllEvents, con))
                {
                    List<EventsModels> lstEvents = new List<EventsModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstEvents.Add(new EventsModels()
                            {
                                EventID = DataConversion.Convert2Int16(reader["EventID"].ToString()),
                                Header = Convert.ToString(reader["Header"] ?? reader["Header"]),
                                Description = Convert.ToString(reader["Description"] ?? reader["Description"]),
                                ImgPath = Convert.ToString(reader["ImgPath"]),
                                EventsDate = Convert.ToString(reader["EventsDate"]) == "" ? (Nullable<DateTime>)null : DataConversion.ConvertToDate(reader["EventsDate"].ToString()),
                                EndDate = Convert.ToString(reader["EndDate"]) == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(reader["EndDate"]),
                                EventsTypeID = DataConversion.Convert2Int16(reader["EventsTypeID"].ToString()),
                                EventsType = Convert.ToString(reader["EventsType"]),
                                VideoURL = Convert.ToString(reader["Video"] ?? reader["Video"]),
                                //PublisherList = new PublishersModels()
                                //{
                                //    Publisher = Convert.ToString(reader["Publisher"]),
                                //    PubLogo = Convert.ToString(reader["Logo"]),
                                //    PublisherID = DataConversion.Convert2Int16(reader["PublisherID"].ToString()),
                                //},
                                UserList = new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int32(reader["AuthorID"].ToString()),
                                    UserName = Convert.ToString(reader["User"]),
                                },
                                UserDetailList = new UserDetailsModels()
                                {
                                    ImgComments = Convert.ToString(reader["ImgComments"]),
                                }
                            });
                        }
                    }
                    return lstEvents;
                }
            }
        }

        public string AddEvents(EventsModels eventsDetails)
        {
            string result = string.Empty;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddEvents, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@Header", eventsDetails.Header.Trim());
                    cmd.Parameters.AddWithValue("@Description", eventsDetails.Description == null ? string.Empty : eventsDetails.Description.Trim());
                    cmd.Parameters.AddWithValue("@EventsTypeID", eventsDetails.EventsTypeID);
                    cmd.Parameters.AddWithValue("@ImgPath", eventsDetails.ImgPath == null ? string.Empty : eventsDetails.ImgPath.Trim());
                    cmd.Parameters.AddWithValue("@URL", eventsDetails.VideoURL == null ? string.Empty : eventsDetails.VideoURL.Trim());
                    cmd.Parameters.AddWithValue("@AuthorId", eventsDetails.AuthorID == 0 ? null : eventsDetails.AuthorID);
                    cmd.Parameters.AddWithValue("@EventsDate", eventsDetails.EventsDate == null ? (object)DBNull.Value : eventsDetails.EventsDate);
                    cmd.Parameters.AddWithValue("@EndDate", eventsDetails.EndDate == null ? (object)DBNull.Value : eventsDetails.EndDate);

                    try
                    {
                        con.Open();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            result = "";
                        }
                        else { result = "Success"; }

                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                        //throw new ArgumentException("The creation was cancelled before it could be saved." + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return result;
        }

        public bool UpdateEvent(EventsModels objEvents)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    using (SqlCommand com = new SqlCommand(SQLObjects.UpdateEvent, sqlConnection))
                    {
                        sqlConnection.Open();
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@EventID", objEvents.EventID);
                        com.Parameters.AddWithValue("@Header", objEvents.Header.Trim());
                        com.Parameters.AddWithValue("@Description", objEvents.Description == null ? string.Empty : objEvents.Description.Trim());
                        com.Parameters.AddWithValue("@EventsTypeID", objEvents.EventsTypeID);
                        com.Parameters.AddWithValue("@AuthorID", objEvents.AuthorID == 0 ? null : objEvents.AuthorID);
                        //com.Parameters.AddWithValue("@PublisherID", objEvents.PublisherList?.PublisherID == 0 ? null : objEvents.PublisherList?.PublisherID);
                        com.Parameters.AddWithValue("@ImgPath", objEvents.ImgPath == null ? string.Empty : objEvents.ImgPath.Trim());
                        com.Parameters.AddWithValue("@Video", objEvents.VideoURL == null ? string.Empty : objEvents.VideoURL.Trim());
                        com.Parameters.AddWithValue("@EventsDate", objEvents.EventsDate);
                        com.Parameters.AddWithValue("@EndDate", objEvents.EndDate == null ? (object)DBNull.Value : objEvents.EndDate);
                        
                        if (com.ExecuteNonQuery() == 0)
                        {
                            return false;
                        }
                        else
                            return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("The creation was cancelled before it could be saved. " + ex.Message);
                }
            }
        }

        public bool DeleteEvent(int id)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand com = new SqlCommand(SQLObjects.DeleteEvent, sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@EventID", id);
                        if (com.ExecuteNonQuery() == 0)
                        {
                            return false;
                        }
                        else
                            return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("The creation was cancelled before it could be saved. " + ex.Message);
                }
            }
        }
    }
}
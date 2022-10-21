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
    public class FeedbackRepository
    {
        public int Add(FeedbackModels feedback)
        {
            int result = 0;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.UserFeedback, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", feedback.UserId);
                    cmd.Parameters.AddWithValue("@Feedback", feedback.Feedback.Trim());
                    cmd.Parameters.AddWithValue("@ProjectID", feedback.ProjectID);
                    cmd.Parameters.AddWithValue("@MasterPageID", feedback.MasterPageID);
                    cmd.Parameters.AddWithValue("@ChildPageID", feedback.ChildPageID);

                    cmd.Parameters.Add("@Result", SqlDbType.Int);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        result = DataConversion.Convert2Int32(cmd.Parameters["@Result"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        result = -1;
                        throw;
                    }
                }
            }
            return result;
        }

        public List<FeedbackModels> GetFeedbackByPageID(Int16 MasterPageID, int ChildPageID)
        {
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetFeedbackByPageID, con))
                    {
                        List<FeedbackModels> lstKTFeedback = new List<FeedbackModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MasterPageID", MasterPageID);
                        cmd.Parameters.AddWithValue("@ChildPageID", ChildPageID);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstKTFeedback.Add(new FeedbackModels()
                                {
                                    FeedbackID = DataConversion.Convert2Int32(reader["FeedbackID"].ToString()),
                                    UserId = DataConversion.Convert2Int32(reader["UserID"].ToString()),
                                    Feedback = Convert.ToString(reader["Feedback"]),
                                    MasterPageID = DataConversion.Convert2TinyInt(reader["MasterPageID"].ToString()),
                                    ChildPageID = DataConversion.Convert2TinyInt(reader["ChildPageID"].ToString()),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                });
                            }
                        }
                        return lstKTFeedback;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<FeedbackModels> Get()
        {
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllFeedback, con))
                    {
                        List<FeedbackModels> lstKTFeedback = new List<FeedbackModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstKTFeedback.Add(new FeedbackModels()
                                {
                                    FeedbackID = DataConversion.Convert2Int32(reader["FeedbackID"].ToString()),
                                    UserId = DataConversion.Convert2Int32(reader["UserID"].ToString()),
                                    Feedback = Convert.ToString(reader["Feedback"]),
                                    MasterPageID = DataConversion.Convert2TinyInt(reader["MasterPageID"].ToString()),
                                    ChildPageID = DataConversion.Convert2TinyInt(reader["ChildPageID"].ToString()),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                });
                            }
                        }
                        return lstKTFeedback;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int feedbackID)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand com = new SqlCommand(SQLObjects.DeleteFeedback, sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@feedbackId", feedbackID);
                        com.ExecuteNonQuery();
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
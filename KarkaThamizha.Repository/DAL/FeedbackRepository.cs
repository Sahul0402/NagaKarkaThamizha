using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace KarkaThamizha.Repository.DAL
{
    public class FeedbackRepository
    {
        #region Page Feedback
        public string AddFeedback(FeedbackModels mdlfeedback)
        {
            string result = "";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddFeedback, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProjectId", mdlfeedback.ProjectID);
                    cmd.Parameters.AddWithValue("@UserID", mdlfeedback.UserId);
                    cmd.Parameters.AddWithValue("@Name", mdlfeedback.Name.Trim());
                    cmd.Parameters.AddWithValue("@EMail", mdlfeedback.EmailID.ToLower().Trim());
                    cmd.Parameters.AddWithValue("@Mobile", mdlfeedback.Mobile.Trim());
                    cmd.Parameters.AddWithValue("@Feedback", string.IsNullOrEmpty(mdlfeedback.Feedback) == true ? "" : mdlfeedback.Feedback.Trim());

                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 7);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        result = Convert.ToString(cmd.Parameters["@Result"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        result = "Fail";
                        throw;
                    }
                }
            }
            return result;
        }

        public List<FeedbackModels> GetAllFeedback(Int16 pageID, Int16 projectID)
        {
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllFeedback, con))
                    {
                        List<FeedbackModels> lstKTFeedback = new List<FeedbackModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@projectID", projectID);
                        cmd.Parameters.AddWithValue("@pageID", pageID);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstKTFeedback.Add(new FeedbackModels()
                                {
                                    FeedbackId = DataConversion.Convert2Int32(reader["FeedbackID"].ToString()),
                                    Feedback = Convert.ToString(reader["Feedback"]),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                    UserId = DataConversion.Convert2Int32(reader["UserID"].ToString()),
                                    Name = Convert.ToString(reader["User"]),
                                    Mobile = Convert.ToString(reader["Mobile"]),
                                    EmailID = Convert.ToString(reader["Email"]),

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
                    using (SqlCommand com = new SqlCommand("dbo.USP_DeleteFeedback", sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@FeedbackId", feedbackID);
                        com.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("The creation was cancelled before it could be saved. " + ex.Message);
                }
            }
        }
        #endregion
    }
}
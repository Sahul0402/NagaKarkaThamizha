using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace KarkaThamizha.Repository.DAL
{
    public class CommentsRepository
    {
        #region User Comments
        public string AddComments(CommentsModels mdlComments)
        {
            string result = "";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddComments, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProjectId", mdlComments.ProjectID);
                    cmd.Parameters.AddWithValue("@UserID", mdlComments.UserID);
                    cmd.Parameters.AddWithValue("@Name", mdlComments.Name.Trim());                    
                    cmd.Parameters.AddWithValue("@EMail", mdlComments.EMail.ToLower().Trim());
                    cmd.Parameters.AddWithValue("@Password", Encryption.Encrypt(mdlComments.Password.Trim()));
                    cmd.Parameters.AddWithValue("@Mobile", mdlComments.Mobile.Trim());
                    cmd.Parameters.AddWithValue("@MasterPageID", mdlComments.MasterPageID);
                    cmd.Parameters.AddWithValue("@ChildPageID", mdlComments.ChildPageID);
                    cmd.Parameters.AddWithValue("@Comments", string.IsNullOrEmpty(mdlComments.Comments) == true ? "" : mdlComments.Comments.Trim());

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

        public List<CommentsModels> GetAllComments()
        {
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllComments, con))
                    {
                        List<CommentsModels> lstKTComments = new List<CommentsModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstKTComments.Add(new CommentsModels()
                                {
                                    CommentsID = DataConversion.Convert2Int32(reader["CommentsID"].ToString()),
                                    ProjectID = DataConversion.Convert2TinyInt(reader["ProjectID"].ToString()),
                                    Name = Convert.ToString(reader["UserName"]),
                                    Mobile = Convert.ToString(reader["Mobile"]),
                                    EMail = Convert.ToString(reader["Email"]),
                                    Comments = Convert.ToString(reader["Feedback"]),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),                                    
                                });
                            }
                        }
                        return lstKTComments;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
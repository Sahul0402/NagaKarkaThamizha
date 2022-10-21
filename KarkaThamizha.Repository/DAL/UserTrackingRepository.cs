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
    public class UserTrackingRepository
    {
        public int SetGetPageViewCount(UserTrackingModels userTrack)
        {
            int count = 0;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddUserTrack, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userTrack.UserID == 0 ? 3 : userTrack.UserID);
                    cmd.Parameters.AddWithValue("@MasterPageID", userTrack.MasterPageID);
                    cmd.Parameters.AddWithValue("@ChildPageID", userTrack.ChildPageID);
                    cmd.Parameters.AddWithValue("@PageView", userTrack.PageView == null ? "" : userTrack.PageView);
                    cmd.Parameters.AddWithValue("@PageLike", userTrack.PageLike == null ? "" : userTrack.PageLike);
                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 30);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        count = DataConversion.Convert2Int32(cmd.Parameters["@Result"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return count;
        }

        public int SetGetPageLikeCount(UserTrackingModels userTrack)
        {
            int count = 0;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddUserTrack, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userTrack.UserID == 0 ? 3 : userTrack.UserID);
                    cmd.Parameters.AddWithValue("@MasterPageID", userTrack.MasterPageID);
                    cmd.Parameters.AddWithValue("@ChildPageID", userTrack.ChildPageID);
                    cmd.Parameters.AddWithValue("@PageView", userTrack.PageView);
                    cmd.Parameters.AddWithValue("@PageLike", userTrack.PageLike);


                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 30);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        count = DataConversion.Convert2Int32(cmd.Parameters["@Result"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return count;
        }
    }
}
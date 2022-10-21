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
    public class UserDetailsRepository
    {
        public string AddUserDetails(UserDetailsModels userDetails)
        {
            string result = string.Empty;
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand com = new SqlCommand(SQLObjects.AddUserDetails, sqlConnection))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@userID", userDetails.UserID);
                    com.Parameters.AddWithValue("@profile", userDetails.Profile == null ? "" : userDetails.Profile.Trim());
                    com.Parameters.AddWithValue("@protocol", userDetails.Protocol == null ? "" : userDetails.Protocol.Trim());
                    com.Parameters.AddWithValue("@website", userDetails.Website == null ? "" : userDetails.Website.Trim());
                    com.Parameters.AddWithValue("@blog", userDetails.Blog == null ? "" : userDetails.Blog.Trim());
                    com.Parameters.AddWithValue("@blogType", userDetails.BlogType == null ? "" : userDetails.BlogType.Trim());
                    com.Parameters.AddWithValue("@faceBook", userDetails.FaceBook == null ? "" : userDetails.FaceBook.Trim());
                    com.Parameters.AddWithValue("@twitter", userDetails.Twitter == null ? "" : userDetails.Twitter.Trim());
                    com.Parameters.AddWithValue("@instagram", userDetails.Instagram == null ? "" : userDetails.Instagram.Trim());
                    com.Parameters.AddWithValue("@pinterest", userDetails.Pinterest == null ? "" : userDetails.Pinterest.Trim());
                    com.Parameters.AddWithValue("@youTube", userDetails.YouTube == null ? "" : userDetails.YouTube.Trim());
                    com.Parameters.AddWithValue("@wikipedia", userDetails.Wikipedia == null ? "" : userDetails.Wikipedia.Trim());
                    com.Parameters.AddWithValue("@dOB", userDetails.DOB == null ? (object)DBNull.Value : userDetails.DOB);
                    com.Parameters.AddWithValue("@dOD", userDetails.DOD == null ? (object)DBNull.Value : userDetails.DOD);
                    com.Parameters.AddWithValue("@imgComments", userDetails.ImgComments == null ? "" : userDetails.ImgComments);
                    com.Parameters.AddWithValue("@imgProfile", userDetails.ImgProfile == null ? "" : userDetails.ImgProfile);
                    com.Parameters.AddWithValue("@address", userDetails.Address == null ? "" : userDetails.Address.Trim());
                    com.Parameters.AddWithValue("@cityID", userDetails.CityID);
                    com.Parameters.AddWithValue("@IsShownInMenu", userDetails.IsShownInMenu);
                    com.Parameters.AddWithValue("@IsMailSubscription", userDetails.IsMailSubscription);
                    com.Parameters.AddWithValue("@reference", userDetails.Reference == null ? "" : userDetails.Reference);

                    com.Parameters.Add("@result", SqlDbType.VarChar, 7);
                    com.Parameters["@result"].Direction = ParameterDirection.Output;
                    try
                    {
                        sqlConnection.Open();
                        com.ExecuteNonQuery();
                        result = Convert.ToString(com.Parameters["@result"].Value);
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                }
            }
            return result;
        }

        public UserDetailsModels GetUsersDetailsByUserId(int userid)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetUsersDetailsByUserId, con))
                {
                    UserDetailsModels userDetail = new UserDetailsModels();

                    cmd.Parameters.AddWithValue("@userId", userid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userDetail.UserID = DataConversion.Convert2Int16(reader["UserID"].ToString());
                            userDetail.Profile = Convert.ToString(reader["Profile"]);
                            userDetail.Website = Convert.ToString(reader["Website"]);
                            userDetail.Blog = Convert.ToString(reader["Blog"]);
                            userDetail.BlogType = Convert.ToString(reader["BlogType"]);
                            userDetail.FaceBook = Convert.ToString(reader["FaceBook"]);
                            userDetail.Twitter = Convert.ToString(reader["Twitter"]);
                            userDetail.Address = Convert.ToString(reader["Address"]);
                            userDetail.DOB = Convert.ToString(reader["DOB"]) == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(Convert.ToString(reader["DOB"]));
                            userDetail.DOD = Convert.ToString(reader["DOD"]) == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(Convert.ToString(reader["DOD"]));
                            userDetail.WriterImage = Convert.ToString(reader["WriterImage"]);
                            userDetail.ImgProfile = Convert.ToString(reader["ImgProfile"]);
                            userDetail.ImgComments = Convert.ToString(reader["ImgComments"]);
                            userDetail.OtherImage = Convert.ToString(reader["OtherImage"]);
                            userDetail.CityID = DataConversion.Convert2Int32(reader["CityID"].ToString());
                            userDetail.IsShownInMenu = Convert.ToBoolean(Convert.ToString(reader["IsShownInMenu"]) == "N" ? false : true);
                            userDetail.Reference = Convert.ToString(reader["Reference"]);
                        }
                    }
                    return userDetail;
                }
            }
        }

        public List<UserDetailsModels> GetUsersDetails(string arr)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetUserDetails, con))
                {
                    List<UserDetailsModels> lstAuthor = new List<UserDetailsModels>();

                    cmd.Parameters.AddWithValue("@authorID", arr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lstAuthor.Add(new UserDetailsModels()
                            {
                                UserID = DataConversion.Convert2Int16(reader["UserID"].ToString()),
                                ImgProfile = Convert.ToString(reader["ImgProfile"]),
                                ImgComments = Convert.ToString(reader["ImgComments"]),
                            });
                        }
                    }
                    return lstAuthor;
                }
            }
        }
    }
}
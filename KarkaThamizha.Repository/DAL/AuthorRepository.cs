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
    public class AuthorRepository
    {
            #region Article - AboutAuthor
        public string AddAuthorProfile(UserModels authorProfile)
        {
            string result = "";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddAuthorProfile, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AuthorID", authorProfile.UserID);
                    cmd.Parameters.AddWithValue("@Header", authorProfile.Header.Trim());
                    cmd.Parameters.AddWithValue("@Description", Convert.ToString(authorProfile.Description.Trim()) ?? string.Empty);
                    cmd.Parameters.AddWithValue("@ReviewedBy", authorProfile.ReviewedBy);
                    //cmd.Parameters.AddWithValue("@MagazineId", authorProfile.Magazine == null ? (Byte.MinValue) : authorProfile.Magazine.MagazineID);
                    cmd.Parameters.AddWithValue("@SourceDate", authorProfile.SourceDate == null ? (object)DBNull.Value : authorProfile.SourceDate);
                    //cmd.Parameters.AddWithValue("@AdminUserID", 1);

                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 30);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            result = cmd.Parameters["@Result"].Value.ToString();
                        }
                        else
                        {
                            result = cmd.Parameters["@Result"].Value.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        result = ex.InnerException.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return result;
        }
        public List<UserModels> GetAuthorBookDetails(int authorID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAuthorBookDetails, con))
                {
                    List<UserModels> lstBooksContent = new List<UserModels>();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksContent.Add(new UserModels()
                            {
                                //AuthorSeriesID = Convert.ToInt16(reader["AuthorSeriesID"]),
                                //SeriesName = Convert.ToString(reader["Header"]),
                                //AuthorName = Convert.ToString(reader["Author"]),
                                //Source = Convert.ToString(reader["Source"]),
                                //WeekDay = Convert.ToString(reader["WeekDays"]),
                                //StartDate = Convert.ToDateTime(reader["StartDate"] == null ? (DateTime?)null : reader["StartDate"]),
                                //EndDate = Convert.ToString(reader["EndDate"]) == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(Convert.ToString(reader["EndDate"])),
                            });
                        }
                    }
                    return lstBooksContent;
                }
            }
        }
        #endregion
    }
}
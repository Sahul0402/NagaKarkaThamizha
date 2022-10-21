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
    public class ArticleRepository
    {
        public List<ArticleModels> GetAllArticle()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllArticle, con))
                {
                    List<ArticleModels> lstBooksContent = new List<ArticleModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksContent.Add(new ArticleModels()
                            {
                                ArticleID = DataConversion.Convert2Int32(reader["ArticleID"].ToString()),
                                ArticleType = new ArticleTypeModels()
                                {
                                    ArticleTypeID = DataConversion.Convert2TinyInt(reader["ArticleTypeID"].ToString()),
                                    ArticleType = Convert.ToString(reader["ArticleType"]),
                                },
                                Header = Convert.ToString(reader["Header"]),
                                Description = Convert.ToString(reader["Description"]),
                                ImgPath = reader["Image"] == null ? "" : Convert.ToString(reader["ImgPath"]),
                                
                                UserDetail = new UserDetailsModels()
                                {
                                    UserID = DataConversion.Convert2Int(Convert.ToString(reader["UserId"])),
                                    ImgComments = Convert.ToString(reader["ImgComments"] == null ? "" : reader["ImgComments"]),
                                },
                                SourceDate = Convert.ToDateTime(reader["SourceDate"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["SourceDate"]),
                            });
                        }
                    }
                    return lstBooksContent;
                }
            }
        }
        public string AddArticle(ArticleModels newArticle)
        {
            string result = "";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddArticle, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ArticleID", newArticle.ArticleID);
                    cmd.Parameters.AddWithValue("@ArticleTypeID", newArticle.ArticleType.ArticleTypeID);
                    cmd.Parameters.AddWithValue("@AuthorID", newArticle.Author.UserID == 0 ? null : Convert.ToString(newArticle.Author.UserID));
                    cmd.Parameters.AddWithValue("@Header", newArticle.Header == null ? "" : Convert.ToString(newArticle.Header).Trim());
                    cmd.Parameters.AddWithValue("@Description", newArticle.Description == null ? "" : Convert.ToString(newArticle.Description).Trim());
                    //cmd.Parameters.AddWithValue("@MagazineID", newArticle.MagazineName.MagazineID);
                    cmd.Parameters.AddWithValue("@SourceDate", newArticle.SourceDate == null ? (object)DBNull.Value : newArticle.SourceDate);
                    cmd.Parameters.AddWithValue("@ImgName", newArticle.ImgPath == null ? "" : Convert.ToString(newArticle.ImgPath).Trim());
                    cmd.Parameters.AddWithValue("@InterviewBy", 1);
                    cmd.Parameters.AddWithValue("@EnteredBy", 1);

                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 7);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        if (cmd.ExecuteNonQuery() == 0)
                            result = cmd.Parameters["@Result"].Value.ToString();
                        else
                            result = cmd.Parameters["@Result"].Value.ToString();
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

        public string AddAuthorSeries(ArticleModels series)
        {
            string result = "";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddAuthorSeries, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Header", series.Header.Trim());
                    cmd.Parameters.AddWithValue("@AuthorID", series.Author.UserID);
                    cmd.Parameters.AddWithValue("@SeriesTypeID", series.SeriesType.SeriesTypeID);
                    cmd.Parameters.AddWithValue("@StartDate", series.SourceDate == null ? (object)DBNull.Value : series.SourceDate);
                    cmd.Parameters.AddWithValue("@EndDate", series.EndDate == null ? (object)DBNull.Value : series.EndDate);

                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 10);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        result = Convert.ToString(cmd.Parameters["@Result"].Value);
                    }
                    catch (Exception ex)
                    {
                        result = ex.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return result;
        }

        #region Article - Interview
        /// <summary>
        /// Get All Interview List
        /// </summary>
        /// <returns></returns>
        public List<ArticleModels> GetAllInterviews()
        {
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllInterviews, con))
                    {
                        List<ArticleModels> lstBooksContent = new List<ArticleModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstBooksContent.Add(new ArticleModels()
                                {
                                    ArticleID = DataConversion.Convert2Int32(reader["InterviewID"].ToString()),
                                    Header = Convert.ToString(reader["Header"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    SourceDate = Convert.ToString(reader["SourceDate"]) == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(Convert.ToString(reader["SourceDate"])),
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    Author = new UserModels()
                                    {
                                        AuthorID = DataConversion.Convert2Int(Convert.ToString(reader["UserID"])),
                                        UserName = Convert.ToString(reader["User"] == null ? "" : reader["User"])
                                    },
                                });
                            }
                        }
                        return lstBooksContent;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string AddInterview(ArticleModels newArticle)
        {
            string result = "";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddInterview, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AuthorID", newArticle.Author.UserID);
                    cmd.Parameters.AddWithValue("@BookDetailsID", newArticle.BookDetailsID == 0 ? null : newArticle.BookDetailsID);
                    cmd.Parameters.AddWithValue("@Header", newArticle.Header.Trim());
                    cmd.Parameters.AddWithValue("@Description", newArticle.Description.Trim() ?? string.Empty);
                    
                    cmd.Parameters.AddWithValue("@SourceDate", newArticle.SourceDate == null ? (object)DBNull.Value : newArticle.SourceDate);
                    cmd.Parameters.AddWithValue("@InterviewBy", newArticle.InterviewBy == 0 ? null : newArticle.InterviewBy);
                    cmd.Parameters.AddWithValue("@AdminUserID", 1);

                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 30);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        result = Convert.ToString(cmd.Parameters["@Result"].Value);
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return result;
        }

        public bool DeleteArticle(int articleId)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand com = new SqlCommand(SQLObjects.DeleteArtcle, sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@ArticleID", articleId);
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
        #endregion
    }
}
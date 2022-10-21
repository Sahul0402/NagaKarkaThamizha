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
    public class BooksReviewRepository
    {
        public string AddBooksReview(BooksReviewModels bookReview)
        {
            string result = string.Empty;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddBooksReview, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookDetailsID", bookReview.BookDetail.BookDetailsID);
                    cmd.Parameters.AddWithValue("@Header", bookReview.Header.Trim());
                    cmd.Parameters.AddWithValue("@BookDescription", bookReview.Description.Trim());
                    cmd.Parameters.AddWithValue("@UserID", bookReview.UserID);

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

        public List<BooksReviewModels> GetAllBooksReviewDetails(int type)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBooksReviewDetails, con))
                {
                    List<BooksReviewModels> lstBooksDetails = new List<BooksReviewModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", type);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksDetails.Add(new BooksReviewModels()
                            {
                                BooksReviewID = DataConversion.Convert2Int32(reader["BooksReviewID"].ToString()),

                                BookName = Convert.ToString(reader["BookName"]),
                                Header = Convert.ToString(reader["Header"]),
                                Description = Convert.ToString(reader["Description"]),
                                UserName = Convert.ToString(reader["Source"]),
                                SourceDate = Convert.ToDateTime(reader["SourceDate"]),
                                BookDetail = new BooksDetailsModels()
                                {
                                    BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                    ImgBookSmallFS = Convert.ToString(reader["ImgBookSmallFS"]),
                                },
                                Category = new CategoryModels()
                                {
                                    Category = Convert.ToString(reader["Category"])
                                }
                            });
                        }
                    }
                    return lstBooksDetails;
                }
            }
        }

        public List<BooksReviewModels> GetAllReviewByBookDetailsID(int bookDetailsID)
        {
            using (SqlConnection con = SQLObject.ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBooksReviewByBookDetailsID, con))
                {
                    List<BooksReviewModels> lstBooksReview = new List<BooksReviewModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookDetailsID", bookDetailsID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksReview.Add(new BooksReviewModels()
                            {
                                BooksReviewID = DataConversion.Convert2Int32(reader["BooksReviewID"].ToString()),
                                BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                BookName = Convert.ToString(reader["BookName"]),
                                Header = Convert.ToString(reader["Header"]),
                                Description = Convert.ToString(reader["Description"]),
                                BookCategoryID = DataConversion.Convert2Int16(reader["CategoryID"].ToString()),
                                BookCategory = Convert.ToString(reader["BookCategory"]),
                                BookDetail = new BooksDetailsModels()
                                { BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()), }
                            });
                        }
                    }
                    return lstBooksReview;
                }
            }
        }

        public List<BooksReviewModels> GetBooksReview(Int16 userTypeID)
        {
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetBooksReview, con))
                    {
                        List<BooksReviewModels> lstBooksReview = new List<BooksReviewModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userTypeID", userTypeID);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstBooksReview.Add(new BooksReviewModels()
                                {
                                    BooksReviewID = DataConversion.Convert2Int32(reader["BooksReviewID"].ToString()),
                                    BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                    Header = Convert.ToString(reader["Header"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    ParentID = DataConversion.Convert2Int16(reader["ParentID"].ToString()),
                                    BookCategoryID = DataConversion.Convert2Int16(reader["CategoryID"].ToString()),
                                    BookCategory = Convert.ToString(reader["BookCategory"]),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                    Type = "Users",
                                    BookDetail = new BooksDetailsModels()
                                    {
                                        BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                        ImgBookSmallFS = Convert.ToString(reader["ImgBookSmallFS"]),
                                    },
                                    Users = new UserModels()
                                    {
                                        UserID = DataConversion.Convert2Int32(reader["UserId"].ToString()),
                                        UserName = Convert.ToString(reader["UserName"].ToString()),
                                    },
                                    UserDetail = new UserDetailsModels()
                                    {
                                        ImgComments = Convert.ToString(reader["ImgComments"]),
                                    },
                                });
                            }
                        }
                        return lstBooksReview;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<BooksReviewModels> GetBooksReviewByReviewID(int booksReviewID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetBooksReviewByReviewID, con))
                {
                    List<BooksReviewModels> lstBooksReview = new List<BooksReviewModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@booksReviewID", booksReviewID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksReview.Add(new BooksReviewModels()
                            {
                                BooksReviewID = DataConversion.Convert2Int32(reader["BooksReviewID"].ToString()),
                                BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                BookName = Convert.ToString(reader["BookName"]),
                                Header = Convert.ToString(reader["Header"]),
                                Description = Convert.ToString(reader["Description"]),                                
                                BookCategoryID = DataConversion.Convert2Int16(reader["CategoryID"].ToString()),
                                BookCategory = Convert.ToString(reader["BookCategory"]),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                Users=new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int32(reader["UserID"].ToString()),
                                    UserName = Convert.ToString(reader["UserName"]),
                                },
                                BookDetail = new BooksDetailsModels()
                                {
                                    BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                }
                            });
                        }
                    }
                    return lstBooksReview;
                }
            }
        }

        public List<BooksReviewModels> GetBooksReviewByMag()
        {
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetBooksReviewByMag, con))
                    {
                        List<BooksReviewModels> lstBooksReview = new List<BooksReviewModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstBooksReview.Add(new BooksReviewModels()
                                {
                                    BooksReviewID = DataConversion.Convert2Int32(reader["BooksReviewID"].ToString()),
                                    BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                    Header = Convert.ToString(reader["Header"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    MagazineID = DataConversion.Convert2Int16(reader["MagazineID"].ToString()),
                                    MagazineName = Convert.ToString(reader["MagazineName"]),
                                    BookCategoryID = DataConversion.Convert2Int16(reader["CategoryID"].ToString()),
                                    BookCategory = Convert.ToString(reader["BookCategory"]),
                                    SourceDate = Convert.ToDateTime(reader["SourceDate"]),
                                    Type = "Magazine",
                                    BookDetail = new BooksDetailsModels()
                                    {
                                        BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                        ImgBookSmallFS = Convert.ToString(reader["ImgBookSmallFS"]),
                                    },
                                    UserDetail = new UserDetailsModels()
                                    {
                                        ImgComments = Convert.ToString(reader["ImgComments"]),
                                    },
                                });
                            }
                        }
                        return lstBooksReview;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public BooksReviewModels GetBooksDetailsByID(int bookDetailsID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("dbo.USP_GetBookDetailsByID", con))
                {
                    BooksReviewModels detail = new BooksReviewModels();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookDetailsID", bookDetailsID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            detail = new BooksReviewModels()
                            {
                                BookDetail = new BooksDetailsModels()
                                {
                                    BookDetailsID = DataConversion.Convert2Int32(Convert.ToString(reader["BookDetailsID"])),
                                    Books = new BooksModels()
                                    {
                                        BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                        Book = Convert.ToString(reader["BookName"]),
                                    },
                                },
                            };
                        }
                    }
                    return detail;
                }
            }
        }

        public string UpdateBookReview(BooksReviewModels mdlBookReview)
        {
            string result = string.Empty;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.UpdateBooksReview, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BooksReviewID", mdlBookReview.BooksReviewID);
                    cmd.Parameters.AddWithValue("@BookDetailsID", mdlBookReview.BookDetail.BookDetailsID);
                    cmd.Parameters.AddWithValue("@Header", mdlBookReview.Header.Trim());
                    cmd.Parameters.AddWithValue("@BookDescription", mdlBookReview.Description.Trim());

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

        public bool DeleteBookReview(int bookReviewid)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand com = new SqlCommand(SQLObjects.DeleteBookReview, sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@booksReviewID", bookReviewid);
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

        public List<BooksReviewModels> GetAllBookReviewsByBookID(int bookID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBookReviewsByBookID, con))
                {
                    List<BooksReviewModels> lstBooksReview = new List<BooksReviewModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookID", bookID);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksReview.Add(new BooksReviewModels()
                            {
                                BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                Header = Convert.ToString(reader["Header"]),
                                UserName = Convert.ToString(reader["Name"]),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                BookDetail = new BooksDetailsModels()
                                {
                                    BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                }
                            });
                        }
                    }
                    return lstBooksReview;
                }
            }
        }
    }
}
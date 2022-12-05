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
    public class BooksDetailsRepository
    {
        public List<BooksDetailsModels> GetAllBooksDetails(string condition)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBooksDetails, con))
                {
                    List<BooksDetailsModels> lstBooksDetails = new List<BooksDetailsModels>();
                    List<UserModels> Users = new List<UserModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Condition", condition);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksDetails.Add(new BooksDetailsModels()
                            {
                                BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                BookCode = Convert.ToString(reader["BookCode"]),
                                Price = DataConversion.Convert2Int16(reader["Price"].ToString()),
                                Pages = DataConversion.Convert2Int16(reader["Pages"].ToString()),
                                NoofCopy = DataConversion.Convert2TinyInt(reader["NoofCopy"].ToString()),
                                ImgBookSmallFS = Convert.ToString(reader["ImgBookSmallFS"]),
                                ImgBookSmallBS = Convert.ToString(reader["ImgBookSmallBS"]),
                                ImgBookLarge = Convert.ToString(reader["ImgBookLarge"]),
                                ISBN13 = Convert.ToString(reader["ISBN13"]),
                                FirstEdition = Convert.ToString(reader["FirstEdition"]),
                                CurrentEdition = Convert.ToString(reader["CurrentEdition"]),
                                Dimensions = Convert.ToString(reader["Dimensions"]),
                                //IsKarkaBook = Convert.ToBoolean(reader["IsKarkaBook"]),
                                Books = new BooksModels()
                                {
                                    BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                    Book = Convert.ToString(reader["BookName"]),
                                    BookDescription = Convert.ToString(reader["BookDescription"]),
                                    Status = Convert.ToString(reader["RecStatus"])
                                },
                                BookFormat = new BookFormatModels()
                                {
                                    BookFormatID = DataConversion.Convert2TinyInt(reader["BookFormatID"].ToString()),
                                    BookFormat = Convert.ToString(reader["BookFormat"]),
                                },
                                Users = new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int(Convert.ToString(reader["UserID"])),
                                    UserName = Convert.ToString(reader["User"]),
                                },
                                Category = new CategoryModels()
                                {
                                    Category = Convert.ToString(reader["CategoryName"]),
                                }

                            });
                        }
                    }
                    return lstBooksDetails;
                }
            }
        }

        public List<BooksDetailsModels> GetRelatedBooksByCategoryID(int categoryID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetRelatedBooksByCategoryID, con))
                {
                    List<BooksDetailsModels> lstBooksDetails = new List<BooksDetailsModels>();
                    List<UserModels> Users = new List<UserModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksDetails.Add(new BooksDetailsModels()
                            {
                                BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                Price = DataConversion.Convert2Int16(reader["Price"].ToString()),
                                ImgBookSmallFS = Convert.ToString(reader["ImgBookSmallFS"]),
                                Books = new BooksModels()
                                {
                                    BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                    Book = Convert.ToString(reader["BookName"])
                                },
                                Users = new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int(Convert.ToString(reader["UserID"])),
                                    UserName = Convert.ToString(reader["User"]),
                                }
                            });
                        }
                    }
                    return lstBooksDetails;
                }
            }
        }

        /// <summary>
        /// Admin-Home Controller
        /// </summary>
        /// <returns></returns>
        public List<BooksDetailsModels> GetAllBookDetails()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBookDetails, con))
                {
                    List<BooksDetailsModels> lstBooksDetails = new List<BooksDetailsModels>();
                    List<UserModels> Users = new List<UserModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksDetails.Add(new BooksDetailsModels()
                            {
                                BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                Price = DataConversion.Convert2Int16(Convert.ToString(reader["Price"])),
                                Pages = DataConversion.Convert2Int16(Convert.ToString(reader["Pages"])),
                                NoofCopy = DataConversion.Convert2TinyInt(Convert.ToString(reader["NoofCopy"])),
                                BookFormatID = DataConversion.Convert2TinyInt(Convert.ToString(reader["BookFormat"])),
                                FirstEdition = Convert.ToString(Convert.ToString(reader["FirstEdition"])),
                                ISBN13 = Convert.ToString(Convert.ToString(reader["ISBN13"])),
                                Books = new BooksModels()
                                {
                                    BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                    Book = Convert.ToString(reader["BookName"]),
                                },
                                
                                BookFormat = new BookFormatModels()
                                {
                                    BookFormatID = DataConversion.Convert2TinyInt(Convert.ToString(reader["BookFormat"])),
                                }
                            });
                        }
                    }
                    return lstBooksDetails;
                }
            }
        }

        //public List<BooksDetailsModels> GetAllBooksByAuthorID(int authorID, int mainbookID)
        //{
        //    try
        //    {
        //        using (SqlConnection con = ConnectionManager.GetConnection())
        //        {
        //            using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBooksByAuthorID, con))
        //            {
        //                List<BooksDetailsModels> lstBooks = null;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@AuthorID", authorID);
        //                cmd.Parameters.AddWithValue("@MainbookID", mainbookID);
        //                con.Open();
        //                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //                {
        //                    lstBooks = new List<BooksDetailsModels>();
        //                    while (reader.Read())
        //                    {
        //                        lstBooks.Add(new BooksDetailsModels()
        //                        {
        //                            Books = new BooksModels()
        //                            {
        //                                BookID = Convert.ToInt32(reader["BookID"]),
        //                                Book = Convert.ToString(reader["BookName"]),
        //                            },
        //                            Category = new CategoryModels()
        //                            {
        //                                CategoryID = DataConversion.Convert2Int16(Convert.ToString(reader["CategoryID"])),
        //                                CategoryName = Convert.ToString(reader["CategoryName"]),
        //                            }
        //                        });
        //                    }
        //                }
        //                return lstBooks;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        /* Calld from below controller           
        1.Karka-->BookDetailsController-->BookDetails 
        
         */
        public List<BooksDetailsModels> GetAllBooksDetailsByAuthorID(int authorID)
        {
            try
            {

                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBooksDetailsByAuthorID, con))
                    {
                        List<BooksDetailsModels> lstBooksDetails = new List<BooksDetailsModels>();
                        List<UserModels> Users = new List<UserModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AuthorID", authorID);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstBooksDetails.Add(new BooksDetailsModels()
                                {
                                    CategoryName = Convert.ToString(reader["CategoryName"]),
                                    CategoryID = DataConversion.Convert2Int16(Convert.ToString(reader["CategoryID"])),
                                    BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                    BookCode = Convert.ToString(reader["BookCode"]),
                                    Price = DataConversion.Convert2Int16(reader["Price"].ToString()),
                                    Pages = DataConversion.Convert2Int16(reader["Pages"].ToString()),
                                    NoofCopy = DataConversion.Convert2TinyInt(reader["NoofCopy"].ToString()),
                                    ImgBookSmallFS = Convert.ToString(reader["ImgBookSmallFS"]),
                                    ImgBookSmallBS = Convert.ToString(reader["ImgBookSmallBS"]),
                                    ImgBookLarge = Convert.ToString(reader["ImgBookLarge"]),
                                    ISBN13 = Convert.ToString(reader["ISBN13"]),
                                    FirstEdition = Convert.ToString(reader["FirstEdition"]),
                                    CurrentEdition = Convert.ToString(reader["CurrentEdition"]),
                                    Dimensions = Convert.ToString(reader["Dimensions"]),
                                    IsKarkaBook = DataConversion.StringToBool(Convert.ToString(reader["IsKarkaBook"])),
                                    UserTypeID = DataConversion.Convert2TinyInt(Convert.ToString(reader["UserTypeID"])),

                                    Books = new BooksModels()
                                    {
                                        BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                        Book = Convert.ToString(reader["BookName"]),
                                        BookDescription = Convert.ToString(reader["BookDescription"]) != null ? Convert.ToString(reader["BookDescription"]).Replace("\n\r", "<br />").Replace("\r", "<br />") : "",
                                    },
                                    //Publisher = new PublishersModels()
                                    //{
                                    //    PublisherID = DataConversion.Convert2Int16(Convert.ToString(reader["PublisherID"])),
                                    //    Publisher = Convert.ToString(reader["Publisher"]),
                                    //},
                                    BookFormat = new BookFormatModels()
                                    {
                                        BookFormat = Convert.ToString(reader["BookFormat"]),
                                    },
                                    Users = new UserModels()
                                    {
                                        UserID = DataConversion.Convert2Int(Convert.ToString(reader["UserID"])),
                                        UserName = Convert.ToString(reader["UserName"]),
                                    },
                                    Category = new CategoryModels()
                                    {
                                        Category = Convert.ToString(reader["CategoryName"]),
                                        CategoryID = DataConversion.Convert2Int16(Convert.ToString(reader["CategoryID"])),
                                    },
                                    BooksReview = new BooksReviewModels()
                                    {
                                        BooksReviewID = DataConversion.Convert2Int32(Convert.ToString(reader["BooksReviewID"])),
                                    },                                    
                                });
                            }
                        }
                        return lstBooksDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<BooksDetailsModels> GetAllBooksDetailsByBookID(int bookID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBooksDetailsByBookID, con))
                {
                    List<BooksDetailsModels> lstBooksDetails = new List<BooksDetailsModels>();
                    List<UserModels> Users = new List<UserModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooksDetails.Add(new BooksDetailsModels()
                            {
                                BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                BookCode = Convert.ToString(reader["BookCode"]),
                                Price = DataConversion.Convert2Int16(reader["Price"].ToString()),
                                Pages = DataConversion.Convert2Int16(reader["Pages"].ToString()),
                                NoofCopy = DataConversion.Convert2TinyInt(reader["NoofCopy"].ToString()),
                                ImgBookSmallFS = Convert.ToString(reader["ImgBookSmallFS"]),
                                ImgBookSmallBS = Convert.ToString(reader["ImgBookSmallBS"]),
                                ImgBookLarge = Convert.ToString(reader["ImgBookLarge"]),
                                ISBN13 = Convert.ToString(reader["ISBN13"]),
                                FirstEdition = Convert.ToString(reader["FirstEdition"]),
                                CurrentEdition = Convert.ToString(reader["CurrentEdition"]),
                                Dimensions = Convert.ToString(reader["Dimensions"]),
                                //IsKarkaBook = Convert.ToBoolean(reader["IsKarkaBook"]),
                                Books = new BooksModels()
                                {
                                    BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                    Book = Convert.ToString(reader["BookName"]),
                                    BookDescription = Convert.ToString(reader["BookDescription"]),
                                    Status = Convert.ToString(reader["RecStatus"])
                                },
                                //Publisher = new PublishersModels()
                                //{
                                //    PublisherID = DataConversion.Convert2Int16(reader["PublisherID"].ToString()),
                                //    Publisher = Convert.ToString(reader["Publisher"]),
                                //},
                                BookFormat = new BookFormatModels()
                                {
                                    BookFormat = Convert.ToString(reader["BookFormat"]),
                                },
                                Users = new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int(Convert.ToString(reader["UserID"])),
                                    UserName = Convert.ToString(reader["User"]),
                                },
                                Category = new CategoryModels()
                                {
                                    Category = Convert.ToString(reader["CategoryName"]),
                                }
                            });
                        }
                    }
                    return lstBooksDetails;
                }
            }
        }

        public BooksDetailsModels GetBooksDetailsByBookID(int BookId)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetBooksDetailsByBookID, con))
                {
                    BooksDetailsModels booksDetails = null;
                    List<UserModels> Users = new List<UserModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookID", BookId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            booksDetails = new BooksDetailsModels()
                            {
                                BookDetailsID = DataConversion.Convert2Int32(reader["BookDetailsID"].ToString()),
                                BookCode = Convert.ToString(reader["BookCode"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Pages = DataConversion.Convert2Int16(reader["Pages"].ToString()),
                                NoofCopy = DataConversion.Convert2TinyInt(reader["NoofCopy"].ToString()),
                                ImgBookSmallFS = Convert.ToString(reader["ImgBookSmallFS"]),
                                ImgBookSmallBS = Convert.ToString(reader["ImgBookSmallBS"]),
                                ImgBookLarge = Convert.ToString(reader["ImgBookLarge"]),
                                ISBN13 = Convert.ToString(reader["ISBN13"]),
                                FirstEdition = Convert.ToString(reader["FirstEdition"]),
                                CurrentEdition = Convert.ToString(reader["CurrentEdition"]),
                                Dimensions = Convert.ToString(reader["Dimensions"]),
                                IsKarkaBook = Convert.ToBoolean(reader["IsKarkaBook"] == null ? false : true),
                                Books = new BooksModels()
                                {
                                    BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                    Book = Convert.ToString(reader["BookName"]),
                                    BookDescription = Convert.ToString(reader["BookDescription"]),
                                    Status = Convert.ToString(reader["RecStatus"])
                                },
                                BookFormat = new BookFormatModels()
                                {
                                    BookFormat = Convert.ToString(reader["Format"]),
                                },
                                Users = new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int32(reader["UserID"].ToString()),
                                    UserName = Convert.ToString(reader["UserName"]),
                                },
                                Category = new CategoryModels()
                                {
                                    CategoryID = DataConversion.Convert2Int16(reader["CategoryID"].ToString()),
                                    SubCategoryID = DataConversion.Convert2Int16(reader["SubCategoryID"].ToString()),
                                    Category = Convert.ToString(reader["CategoryName"]),
                                }
                            };
                        }
                    }
                    return booksDetails;
                }
            }
        }

        public string AddBooksDetails(BooksDetailsModels bookDetails)
        {
            string result = "";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddBooksDetails, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookDetailsID", bookDetails.BookDetailsID);
                    cmd.Parameters.AddWithValue("@BookID", bookDetails.Books.BookID);
                    cmd.Parameters.AddWithValue("@BookCode", bookDetails.BookCode == null ? "" : bookDetails.BookCode.Trim());
                    cmd.Parameters.AddWithValue("@Price", bookDetails.Price);
                    cmd.Parameters.AddWithValue("@Pages", bookDetails.Pages);
                    cmd.Parameters.AddWithValue("@PublisherID", bookDetails.PublisherID);
                    cmd.Parameters.AddWithValue("@NoofCopy", bookDetails.NoofCopy);
                    cmd.Parameters.AddWithValue("@BookFormatID", bookDetails.BookFormatID);
                    cmd.Parameters.AddWithValue("@ImgBookSmallFS", bookDetails.ImgBookSmallFS == null ? string.Empty : bookDetails.ImgBookSmallFS.Trim());
                    cmd.Parameters.AddWithValue("@ImgBookSmallBS", bookDetails.ImgBookSmallBS == null ? "" : bookDetails.ImgBookSmallBS.Trim());
                    cmd.Parameters.AddWithValue("@ImgBookLarge", bookDetails.ImgBookLarge ?? string.Empty);
                    cmd.Parameters.AddWithValue("@ISBN13", bookDetails.ISBN13 == null ? string.Empty : bookDetails.ISBN13.Trim());
                    cmd.Parameters.AddWithValue("@FirstEdition", bookDetails.FirstEdition == null ? string.Empty : bookDetails.FirstEdition.Trim());
                    cmd.Parameters.AddWithValue("@CurrentEdition", bookDetails.CurrentEdition == null ? string.Empty : bookDetails.CurrentEdition.Trim());
                    cmd.Parameters.AddWithValue("@Dimensions", bookDetails.Dimensions == null ? string.Empty : bookDetails.Dimensions.Trim());
                    cmd.Parameters.AddWithValue("@IsKarkaBook", (bool)bookDetails.IsKarkaBook);
                    cmd.Parameters.AddWithValue("@EnteredBy", 1);

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
                        result = "Error on Saving the Book Details." + ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return result;
        }
    }
}
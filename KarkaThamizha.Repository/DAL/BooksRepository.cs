using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace KarkaThamizha.Repository.DAL
{
    public class BooksRepository
    {
        //public List<BooksModels> GetAllBooks()
        //{
        //    SqlTransaction trans;
        //    using (SqlConnection con = ConnectionManager.GetConnection())
        //    {
        //        using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBooks, con))
        //        {
        //            List<BooksModels> lstBooks = new List<BooksModels>();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            con.Open();
        //            trans = con.BeginTransaction();
        //            cmd.Transaction = trans;

        //            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //            {
        //                while (reader.Read())
        //                {
        //                    lstBooks.Add(new BooksModels()
        //                    {
        //                        BookID = Convert.ToInt32(reader["BookID"]),
        //                        Book = Convert.ToString(reader["Name"]),
        //                        BookDescription = Convert.ToString(reader["BookDescription"]),
        //                        English = Convert.ToString(reader["English"]),
        //                        Tanglish = Convert.ToString(reader["Tanglish"]),
        //                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
        //                    });
        //                }
        //            }
        //            return lstBooks;
        //        }
        //    }
        //}

        public List<BooksModels> GetAllBooksWithAuthor()
        {
            List<BooksModels> Books = new List<BooksModels>();
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBooksWithAuthor, con))
                {
                    List<BooksModels> lstBooks = new List<BooksModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooks.Add(new BooksModels()
                            {
                                BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                Book = Convert.ToString(reader["BookName"]),
                                BookDescription = Convert.ToString(reader["BookDescription"]),
                                Name = Convert.ToString(reader["Name"]),
                                Tanglish = Convert.ToString(reader["Tanglish"]),
                                CreatedDate = DataConversion.ConvertToDate(reader["CreatedDate"].ToString()),
                                Users = new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int32(Convert.ToString(reader["AuthorId"])),
                                    UserName = Convert.ToString(reader["Author"] == null ? "" : reader["Author"]),
                                    UserType = new UserTypeModels()
                                    {
                                        UserTypeID = DataConversion.Convert2TinyInt(reader["UserTypeID"].ToString())
                                    }
                                },
                            });
                        }
                    }
                    return lstBooks;
                }
            }
        }

        public int AddBooks(BooksModels book)
        {
            int result;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddBooks, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Book", book.Book.Trim());
                    cmd.Parameters.AddWithValue("@Name", book.Name == null ? string.Empty : book.Name.Trim());
                    cmd.Parameters.AddWithValue("@Tanglish", book.Tanglish == null ? "" : book.Tanglish.Trim());
                    cmd.Parameters.AddWithValue("@BookDescription", book.BookDescription == null ? "" : book.BookDescription.Trim());                                        
                    cmd.Parameters.AddWithValue("@AdminID", 1);
                    cmd.Parameters.Add("@RecStatus", SqlDbType.Char, 1).Value = Convert.ToString(book.Status);

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
                        throw;
                    }
                }
            }
            return result;
        }

        public bool UpdateBook(int bookID, BooksModels book)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    using (SqlCommand com = new SqlCommand(SQLObjects.UpdateBook, sqlConnection))
                    {
                        sqlConnection.Open();
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@BookID", book.BookID);                        
                        com.Parameters.AddWithValue("@Book", book.Book.Trim());
                        com.Parameters.AddWithValue("@Name", book.Name == null ? string.Empty : book.Name.Trim());
                        com.Parameters.AddWithValue("@Tanglish", book.Tanglish == null ? string.Empty : book.Tanglish.Trim());
                        com.Parameters.AddWithValue("@BookDescription", book.BookDescription == null ? "" : book.BookDescription.Trim());
                        com.Parameters.Add("@RecStatus", SqlDbType.Char, 1).Value = Convert.ToString(book.Status);
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

        public bool UpdateBookStatus(int bookID)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    using (SqlCommand com = new SqlCommand(SQLObjects.UpdateBookStatus, sqlConnection))
                    {
                        sqlConnection.Open();
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@BookID", bookID);
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

        public bool DeleteBook(int id)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand com = new SqlCommand(SQLObjects.DeleteBook, sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@BookID", id);
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

        public List<BooksModels> GetSelectedAuthor(int bookID)
        {
            List<BooksModels> Books ;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetSelectedAuthor, con))
                {
                    List<BooksModels> lstBooks = new List<BooksModels>();
                    cmd.Parameters.AddWithValue("@bookid", bookID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooks.Add(new BooksModels()
                            {
                                BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                Users = new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int32(reader["AuthorID"].ToString()),
                                    UserType = new UserTypeModels()
                                    {
                                        UserTypeID = Convert.ToByte(reader["UserTypeID"]),
                                    }
                                }
                            });
                        }
                    }
                    return lstBooks;
                }
            }
        }

        public List<BooksModels> GetAllBooksWithCategory()
        {
            SqlTransaction trans;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBooksWithCategory, con))
                {
                    List<BooksModels> lstBooks = new List<BooksModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Transaction = trans;

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBooks.Add(new BooksModels()
                            {
                                BookID = DataConversion.Convert2Int32(reader["BookID"].ToString()),
                                Book = Convert.ToString(reader["Book"]),
                                Users = new UserModels()
                                {
                                    UserName = Convert.ToString(reader["UserName"])
                                },
                                Category = new CategoryModels()
                                {
                                    Category = Convert.ToString(reader["CategoryName"])
                                }
                            });
                        }
                    }
                    return lstBooks;
                }
            }
        }
    }
}

using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using KarkaThamizha.Repository.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace KarkaThamizha.Repository.DAL
{
    public class CategoryRepository
    {
        public List<CategoryModels> GetAllCategory()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllCategory, con))
                {
                    List<CategoryModels> lstCategory = new List<CategoryModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstCategory.Add(new CategoryModels()
                            {
                                CategoryID = DataConversion.Convert2Int16(reader["CategoryID"].ToString()),
                                Category = Convert.ToString(reader["Category"]),
                                //CategoryEng = Convert.ToString(reader["CategoryEng"]),
                                ParentID = DataConversion.Convert2Int16(reader["ParentID"].ToString()),
                                //ShowInMenu = Convert.ToBoolean(Convert.ToString(reader["ShowMenu"]) == "N" ? false : true),
                            });
                        }
                    }
                    return lstCategory;
                }
            }
        }

        public bool UpdateCategory(int categoryID, CategoryModels category)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand com = new SqlCommand(SQLObjects.UpdateCategory, sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                        com.Parameters.AddWithValue("@Category", category.Category.Trim());
                        com.Parameters.AddWithValue("@Name", category.Name.Trim());
                        com.Parameters.AddWithValue("@ShowMenu", category.ShowInMenu);
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

        public bool AddCategory(BooksModels books)
        {
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand com = new SqlCommand(SQLObjects.AddCategory, sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@BookID", books.BookID);
                        com.Parameters.AddWithValue("@CategoryID", books.Categories.CategoryID);
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

        public string AddCategory(DataTable dtCategory)
        {
            string result = string.Empty;
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddBooksCategory, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var categoryParam = cmd.Parameters.AddWithValue("@Categories", dtCategory);
                    categoryParam.SqlDbType = SqlDbType.Structured;
                    try
                    {
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            result = "";
                        }
                        else { result = "Success"; }
                    }
                    catch (SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            errorMessages.Append("Index #" + i + "\n" +
                                "Message: " + ex.Errors[i].Message + "\n" +
                                "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                                "Source: " + ex.Errors[i].Source + "\n" +
                                "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        result = errorMessages.ToString();
                    }
                }
            }
            return result;
        }

        public string GetSelectedCategory1(int bookID)
        {
            string categories = "";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetSelectedCategory, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookid", bookID);
                    cmd.Parameters.Add("@CategoriesIds", SqlDbType.VarChar, 30);
                    cmd.Parameters["@CategoriesIds"].Direction = ParameterDirection.Output;

                    con.Open();
                    if (cmd.ExecuteNonQuery() == 0)
                        categories = cmd.Parameters["@CategoriesIds"].Value.ToString();
                    else
                        categories = cmd.Parameters["@CategoriesIds"].Value.ToString();

                    return categories;
                }
            }
        }

        public DataTable GetSelectedCategory(int bookID)
        {
            DataTable dtCategory = null;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetSelectedCategories, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookid", bookID);

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        dtCategory = new DataTable();
                        dtCategory.Load(reader);
                    }
                    return dtCategory;
                }
            }
        }

        public List<CategoryModels> GetCategoryByBookID(int bookID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetCategoryByBookID, con))
                {
                    List<CategoryModels> lstCategory = new List<CategoryModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstCategory.Add(new CategoryModels()
                            {
                                CategoryID = DataConversion.Convert2Int16(reader["CategoryID"].ToString()),
                                Category = Convert.ToString(reader["Name"]),
                                ParentID = DataConversion.Convert2Int16(reader["ParentID"].ToString()),
                            });
                        }
                    }
                    return lstCategory;
                }
            }
        }
    }
}
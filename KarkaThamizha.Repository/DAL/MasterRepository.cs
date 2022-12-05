using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static KarkaThamizha.Object.Models.MasterModels;

namespace KarkaThamizha.Repository.DAL
{
    public class MasterRepository
    {
        #region Country
        public List<MasterModels.CountryModels> GetAllCountries()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllCountries, con))
                {
                    List<MasterModels.CountryModels> lstCountry = new List<MasterModels.CountryModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstCountry.Add(new MasterModels.CountryModels()
                            {
                                CountryID = Convert.ToByte(reader["CountryID"]),
                                Country = Convert.ToString(reader["Country"]),
                            });
                        }
                    }
                    return lstCountry;
                }
            }
        }
        #endregion

        #region StateByCountryID
        public List<MasterModels.StateModels> GetStatesByCountryID(int countryID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetStatesByCountryID, con))
                {
                    List<MasterModels.StateModels> lstState = new List<MasterModels.StateModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@countryID", countryID);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstState.Add(new MasterModels.StateModels()
                            {
                                StateID = DataConversion.Convert2Int16(reader["StateID"].ToString()),
                                State = Convert.ToString(reader["State"]),
                            });
                        }
                    }
                    return lstState;
                }
            }
        }
        #endregion

        #region GetCityByStateID
        public List<MasterModels.CityModels> GetCityByStateID(int countryID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetCityByStateID, con))
                {
                    List<MasterModels.CityModels> lstCity = new List<MasterModels.CityModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@stateID", countryID);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstCity.Add(new MasterModels.CityModels()
                            {
                                CityID = DataConversion.Convert2Int16(reader["CityID"].ToString()),
                                City = Convert.ToString(reader["City"]),
                            });
                        }
                    }
                    return lstCity;
                }
            }
        } 
        #endregion

        #region Special Name
        public List<MasterModels.SpecialNameModels> GetAllSpecialName()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllSpecialName, con))
                {
                    List<MasterModels.SpecialNameModels> lstSpName = new List<MasterModels.SpecialNameModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstSpName.Add(new MasterModels.SpecialNameModels()
                            {
                                SpecialNameID = Convert.ToByte(reader["SpecialNameID"]),
                                SpecialName = Convert.ToString(reader["SpecialName"]),
                            });
                        }
                    }
                    return lstSpName;
                }
            }
        }
        #endregion

        #region Profession
        public List<MasterModels.ProfessionModels> GetAllProfession()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllProfession, con))
                {
                    List<MasterModels.ProfessionModels> lstSpName = new List<MasterModels.ProfessionModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstSpName.Add(new MasterModels.ProfessionModels()
                            {
                                ProfessionID = Convert.ToByte(reader["ProfessionID"]),
                                Profession = Convert.ToString(reader["Profession"]),
                            });
                        }
                    }
                    return lstSpName;
                }
            }
        }
        #endregion

        #region BookFormat
        public List<BookFormatModels> GetAllBookFormat()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllBookFormat, con))
                {
                    List<BookFormatModels> lstBookFormat = new List<BookFormatModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstBookFormat.Add(new BookFormatModels()
                            {
                                BookFormatID = Convert.ToByte(reader["BookFormatID"]),
                                BookFormat = Convert.ToString(reader["BookFormat"]),
                            });
                        }
                    }
                    return lstBookFormat;
                }
            }
        }
        #endregion

        #region Article Type
        public List<ArticleTypeModels> GetAllArticleType()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllArticleType, con))
                {
                    List<ArticleTypeModels> lstArticleType = new List<ArticleTypeModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstArticleType.Add(new ArticleTypeModels()
                            {
                                ArticleTypeID = Convert.ToByte(reader["ArticleTypeID"]),
                                ArticleType = Convert.ToString(reader["ArticleType"]),
                            });
                        }
                    }
                    return lstArticleType;
                }
            }
        }
        #endregion

        #region Series Type        
        public List<MasterModels.SeriesTypeModels> GetAllSeriesType()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllSeriesType, con))
                {
                    List<MasterModels.SeriesTypeModels> lstSeriesType = new List<MasterModels.SeriesTypeModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstSeriesType.Add(new MasterModels.SeriesTypeModels()
                            {
                                SeriesTypeID = Convert.ToByte(reader["SeriesTypeID"]),
                                SeriesType = Convert.ToString(reader["SeriesType"]),
                            });
                        }
                    }
                    return lstSeriesType;
                }
            }
        }
        #endregion

        #region Cache Name
        public List<MasterModels.CacheModels> GetAllCacheDetails()
        {
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllCacheName, con))
                    {
                        List<MasterModels.CacheModels> lstCacheName = new List<MasterModels.CacheModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstCacheName.Add(new MasterModels.CacheModels()
                                {
                                    CacheID = DataConversion.Convert2TinyInt(reader["CacheNameID"].ToString()),
                                    Code = Convert.ToString(reader["Code"]),
                                    CacheName = Convert.ToString(reader["CacheName"]),
                                });
                            }
                        }
                        return lstCacheName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ToDOList
        public List<ToDoModels> GetAllToDo()
        {
            List<ToDoModels> lstToDo = new List<ToDoModels>();

            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllToDo, con))
                {
                    List<ToDoModels> lstBooksContent = new List<ToDoModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstToDo.Add(new ToDoModels()
                                {
                                    ToDoListID = DataConversion.Convert2Int32(reader["ToDoListID"].ToString()),
                                    Description = Convert.ToString(reader["Description"]),
                                    TimeInterval = Convert.ToString(reader["TimeInterval"]),
                                });
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    return lstToDo;
                }
            }

        }

        public int AddToDoList(string description)
        {
            int result;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddToDoList, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@description", description.Trim());

                    try
                    {
                        con.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return result;
        }
        #endregion

        public List<BreakingNewsModels> GetLatestNews()
        {
            List<BreakingNewsModels> lstBreakingNews = new List<BreakingNewsModels>();

            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetLatestNews, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstBreakingNews.Add(new BreakingNewsModels()
                                {
                                    BreakingNewsID = DataConversion.Convert2Int32(reader["BreakingNewsID"].ToString()),
                                    Description = Convert.ToString(reader["Header"]),
                                    Type = Convert.ToString(reader["Type"]),
                                    UserID = Convert.ToInt32(reader["AuthorID"]),
                                    UserName = Convert.ToString(reader["UserName"]),
                                });
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    return lstBreakingNews;
                }
            }
        }

        
    }
}
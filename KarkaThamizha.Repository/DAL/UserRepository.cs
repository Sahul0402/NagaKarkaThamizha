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
    public class UserRepository
    {
        public int AddUser(UserModels list)
        {
            int Result = 0;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddUser, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = list.UserID == 0 ? 0 : list.UserID;
                    cmd.Parameters.Add("@Initial", SqlDbType.NVarChar, 14).Value = list.Initial?.Trim() ?? null;
                    cmd.Parameters.Add("@User", SqlDbType.NVarChar, 100).Value = list.UserName.Trim();
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 35).Value = list.Name.Trim();
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 40).Value = list.Email.ToLower().Trim();
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 60).Value = Encryption.Encrypt(list.Password.Trim());
                    cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 20).Value = list.Mobile.Trim();
                    cmd.Parameters.Add("@ProfessionID", SqlDbType.TinyInt, 1).Value = list.Profession.ProfessionID;
                    cmd.Parameters.Add("@UserTypeID", SqlDbType.TinyInt, 1).Value = list.UserType.UserTypeID;
                    cmd.Parameters.Add("@SpecialNameID", SqlDbType.TinyInt, 2).Value = list.SpecialName?.SpecialNameID == null ? null : list.SpecialName?.SpecialNameID;

                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 30);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Result = DataConversion.Convert2Int32(cmd.Parameters["@Result"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return Result;
        }

        public List<UserModels> GetAllUsers()
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllUsers, con))
                {
                    List<UserModels> lstAuthor = new List<UserModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstAuthor.Add(new UserModels()
                            {
                                UserID = DataConversion.Convert2Int16(reader["UserID"].ToString()),
                                Initial = Convert.ToString(reader["Initial"]),
                                UserName = Convert.ToString(reader["UserName"]),
                                Name = Convert.ToString(reader["Name"]),
                                Email = Convert.ToString(reader["MailID"]),
                                Password = Encryption.Decrypt(Convert.ToString(reader["Password"])),
                                Mobile = Convert.ToString(reader["Mobile"]),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                UserType = new UserTypeModels()
                                {
                                    UserTypeID = DataConversion.Convert2TinyInt(reader["UserTypeID"].ToString()),
                                },
                                Profession = new MasterModels.ProfessionModels()
                                {
                                    ProfessionID = DataConversion.Convert2TinyInt(reader["ProfessionID"].ToString()),
                                    Profession = Convert.ToString(reader["Profession"]),
                                },
                                SpecialName = new MasterModels.SpecialNameModels()
                                { SpecialNameID = DataConversion.Convert2TinyInt(reader["SpecialNameID"].ToString()), },
                                UserDetail = new UserDetailsModels()
                                {
                                    Profile = Convert.ToString(reader["Profile"]),
                                    Protocol = Convert.ToString(reader["Protocol"]),
                                    Website = Convert.ToString(reader["Website"]),
                                    Blog = Convert.ToString(reader["Blog"]),
                                    BlogType = Convert.ToString(reader["BlogType"]),
                                    FaceBook = Convert.ToString(reader["FaceBook"]),
                                    Twitter = Convert.ToString(reader["Twitter"]),
                                    Instagram = Convert.ToString(reader["Instagram"]),
                                    Pinterest = Convert.ToString(reader["Pinterest"]),
                                    YouTube = Convert.ToString(reader["YouTube"]),
                                    Wikipedia = Convert.ToString(reader["Wikipedia"]),
                                    Address = Convert.ToString(reader["Address"]),
                                    DOB = Convert.ToString(reader["DOB"]) == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(Convert.ToString(reader["DOB"])),
                                    DOD = Convert.ToString(reader["DOD"]) == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(Convert.ToString(reader["DOD"])),
                                    ImgProfile = Convert.ToString(reader["ImgProfile"]),
                                    ImgComments = Convert.ToString(reader["ImgComments"]),
                                    CountryID = DataConversion.Convert2Int16(Convert.ToString(reader["CountryID"])),
                                    StateID = DataConversion.Convert2Int16(Convert.ToString(reader["StateID"])),
                                    CityID = DataConversion.Convert2Int16(Convert.ToString(reader["CityID"])),
                                    IsShownInMenu = DataConversion.StringToBool(Convert.ToString(reader["IsShownInMenu"])) == true ? true : false,
                                    IsMailSubscription = DataConversion.StringToBool(Convert.ToString(reader["IsMailSubscription"])) == true ? true : false,
                                    Reference = Convert.ToString(reader["Reference"]),
                                },
                            });
                        }
                    }
                    return lstAuthor;
                }
            }
        }

        public UserModels GetUserByUserID(int userID)
        {
            UserModels user = null;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetUserByUserID, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            user = new UserModels()
                            {
                                UserID = DataConversion.Convert2Int16(reader["UserID"].ToString()),
                                Initial = Convert.ToString(reader["Initial"]),
                                UserName = Convert.ToString(reader["UserName"]),
                                Name = Convert.ToString(reader["Name"]),
                                Email = Convert.ToString(reader["MailID"]),
                                Password = Encryption.Decrypt(Convert.ToString(reader["Password"])),
                                Mobile = Convert.ToString(reader["Mobile"]),
                                UserType = new UserTypeModels()
                                {
                                    UserTypeID = DataConversion.Convert2TinyInt(reader["UserTypeID"].ToString()),
                                },
                                SpecialName = new MasterModels.SpecialNameModels()
                                {
                                    SpecialNameID = DataConversion.Convert2TinyInt(reader["SpecialNameID"].ToString()),
                                },
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                Profession = new MasterModels.ProfessionModels()
                                {
                                    ProfessionID = DataConversion.Convert2TinyInt(reader["ProfessionID"].ToString()),
                                },
                                UserDetail = new UserDetailsModels()
                                {
                                    Website = Convert.ToString(reader["Website"]),
                                    Blog = Convert.ToString(reader["Blog"]),
                                    BlogType = Convert.ToString(reader["BlogType"]),
                                    FaceBook = Convert.ToString(reader["FaceBook"]),
                                    Twitter = Convert.ToString(reader["Twitter"]),
                                    Pinterest = Convert.ToString(reader["Pinterest"]),
                                    YouTube = Convert.ToString(reader["YouTube"]),
                                    Instagram = Convert.ToString(reader["Instagram"]),
                                    Wikipedia = Convert.ToString(reader["Wikipedia"]),
                                    Address = Convert.ToString(reader["Address"]),
                                    DOB = DataConversion.ConvertToDate1(Convert.ToString(reader["DOB"])),
                                    DOD = DataConversion.ConvertToDate2(Convert.ToString(reader["DOD"])),
                                    ImgProfile = Convert.ToString(reader["ImgProfile"]),
                                    ImgComments = Convert.ToString(reader["ImgComments"]),
                                    CountryID = DataConversion.Convert2Int16(reader["CountryID"].ToString()),
                                    StateID = DataConversion.Convert2Int16(reader["StateID"].ToString()),
                                    CityID = DataConversion.Convert2Int16(reader["CityID"].ToString()),
                                    Profile = Convert.ToString(reader["Profile"]),
                                    IsMailSubscription = DataConversion.StringToBool(Convert.ToString(reader["IsMailSubscription"])) == true ? true : false,
                                },

                            };
                        }
                    }
                    return user;
                }
            }
        }

        public int CheckUserExists(UserModels loginUser)
        {
            string result = string.Empty;
            int userId = 0;
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    using (SqlCommand com = new SqlCommand("dbo.USP_ValidateUser", sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@Username", loginUser.UserName.Trim());
                        com.Parameters.AddWithValue("@Password", Encryption.Encrypt(loginUser.Password.Trim()));
                        sqlConnection.Open();
                        userId = DataConversion.Convert2Int32(com.ExecuteScalar().ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("The creation was cancelled before it could be saved. " + ex.Message);
                }
            }
            return userId;
        }

        public bool CheckEmailExists(string mail)
        {
            bool flag = false;
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    using (SqlCommand com = new SqlCommand(SQLObjects.CheckMailExists, sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@EMail", mail.Trim());
                        sqlConnection.Open();
                        flag = Convert.ToBoolean(com.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("The creation was cancelled before it could be saved. " + ex.Message);
                }
            }
            return flag;
        }

        internal string DeleteAuthor(int userId)
        {
            string result = string.Empty;
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand com = new SqlCommand("dbo.USP_DeleteUser", sqlConnection))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@UserID", userId);
                        com.ExecuteNonQuery();
                        result = "";
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("The creation was cancelled before it could be saved. " + ex.Message);
                }
            }
            return result;
        }

        public string UpdateUser(UserModels user)
        {
            string result = string.Empty;
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand com = new SqlCommand("dbo.USP_UpdateAuthor", sqlConnection))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@AuthorID", user.UserID);
                    com.Parameters.AddWithValue("@User", user.UserName.Trim());
                    com.Parameters.AddWithValue("@Name", user.Name == null ? "" : user.Name.Trim());
                    com.Parameters.AddWithValue("@EMail", user.Email == null ? "" : user.Email.ToLower().Replace(" ", "").Trim());
                    com.Parameters.AddWithValue("@Password", user.Password == null ? "" : Encryption.Encrypt(user.Password.Trim()));
                    com.Parameters.AddWithValue("@Mobile", user.Mobile == null ? "" : user.Mobile.Trim());
                    com.Parameters.AddWithValue("@UserTypeID", user.Profession.ProfessionID);
                    com.Parameters.AddWithValue("@IsMailSubscription", user.UserDetail.IsMailSubscription);
                    com.Parameters.AddWithValue("@Website", user.UserDetail.Website == null ? "" : user.UserDetail.Website.Trim());
                    com.Parameters.AddWithValue("@Blog", user.UserDetail.Blog == null ? "" : user.UserDetail.Blog.Trim());
                    com.Parameters.AddWithValue("@FaceBook", user.UserDetail.FaceBook == null ? "" : user.UserDetail.FaceBook.Trim());
                    com.Parameters.AddWithValue("@Twitter", user.UserDetail.Twitter == null ? "" : user.UserDetail.Twitter.Trim());
                    com.Parameters.AddWithValue("@Address", user.UserDetail.Address == null ? "" : user.UserDetail.Address.Trim());
                    com.Parameters.AddWithValue("@DOB", user.UserDetail.DOB == null ? (object)DBNull.Value : user.UserDetail.DOD);
                    com.Parameters.AddWithValue("@DOD", user.UserDetail.DOD == null ? (object)DBNull.Value : user.UserDetail.DOD);
                    com.Parameters.AddWithValue("@ImgProfile", user.UserDetail.ImgProfile == null ? "" : user.UserDetail.ImgProfile);
                    com.Parameters.AddWithValue("@ImgComments", user.UserDetail.ImgComments == null ? "" : user.UserDetail.ImgComments);
                    com.Parameters.AddWithValue("@IsShownInMenu", user.UserDetail.IsShownInMenu);

                    com.Parameters.Add("@Result", SqlDbType.VarChar, 30);
                    com.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        sqlConnection.Open();
                        com.ExecuteNonQuery();
                        result = Convert.ToString(com.Parameters["@Result"].Value);
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                }
            }
            return result;
        }

        public List<UserModels> GetUserByAuthorType()
        {
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetUserByAuthorType, con))
                    {
                        List<UserModels> lstUser = new List<UserModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstUser.Add(new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int16(reader["UserID"].ToString()),
                                    UserName = Convert.ToString(reader["UserName"]),
                                });
                            }
                        }
                        return lstUser;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string AddBooksAuthor(DataTable dtAuthor)
        {
            string result = string.Empty;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddBooksAuthor, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var authorParam = cmd.Parameters.AddWithValue("@Authors", dtAuthor);
                    authorParam.SqlDbType = SqlDbType.Structured;
                    try
                    {
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            result = "";
                        }
                        else { result = "Success"; }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return result;
        }

        public List<UserModels> GetAuthorByBookID(int bookID)
        {
            UserModels mdlUser = new UserModels();
            List<UserModels> lstUser = null;

            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAuthorByBookID, con))
                {
                    lstUser = new List<UserModels>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookID", bookID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            lstUser.Add(new UserModels()
                            {
                                UserID = DataConversion.Convert2Int32(reader["AuthorID"].ToString()),
                                UserName = Convert.ToString(reader["Author"]),
                            });
                        }
                    }
                    return lstUser;
                }
            }
        }

        public List<UserModels> GetAllAuthorsProfile(int userTypeID)
        {
            List<UserModels> lstAuthor = null;
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAllAuthorsProfile, con))
                    {
                        lstAuthor = new List<UserModels>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserTypeID", userTypeID);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                lstAuthor.Add(new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int16(reader["UserID"].ToString()),
                                    UserName = Convert.ToString(reader["UserName"].ToString().Trim()),
                                    Name = Convert.ToString(reader["Name"]),
                                    UserDetail = new UserDetailsModels()
                                    {
                                        UserDetailsID = DataConversion.Convert2Int32(reader["UserDetailID"].ToString()),
                                        Profile = Convert.ToString(reader["Profile"]),
                                        Protocol = Convert.ToString(reader["Protocol"]),
                                        Website = Convert.ToString(reader["Website"]),
                                        Blog = Convert.ToString(reader["Blog"]),
                                        BlogType = Convert.ToString(reader["BlogType"]),
                                        FaceBook = Convert.ToString(reader["FaceBook"]),
                                        Twitter = Convert.ToString(reader["Twitter"]),
                                        YouTube = Convert.ToString(reader["YouTube"]),
                                        Pinterest = Convert.ToString(reader["Pinterest"]),
                                        Instagram = Convert.ToString(reader["Instagram"]),
                                        Wikipedia = Convert.ToString(reader["Wikipedia"]),
                                        ImgProfile = Convert.ToString(reader["ImgProfile"]),
                                        ImgComments = Convert.ToString(reader["ImgComments"]),
                                        DOB = reader["DOB"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["DOB"],
                                        DOD = reader["DOD"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["DOD"]
                                    },
                                });
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstAuthor;
        }

        public UserModels GetAuthorsProfileByID(int userID)
        {
            UserModels authorInfo = null;
            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SQLObjects.GetAuthorsProfileByID, con))
                    {
                        authorInfo = new UserModels();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userID", userID);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                authorInfo = new UserModels()
                                {
                                    UserID = DataConversion.Convert2Int32(reader["UserID"].ToString()),
                                    UserName = Convert.ToString(reader["UserName"]),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                    UserDetail = new UserDetailsModels()
                                    {
                                        UserDetailsID = DataConversion.Convert2Int32(reader["UserDetailID"].ToString()),
                                        Profile = Convert.ToString(reader["Profile"]),
                                        Website = Convert.ToString(reader["Website"]),
                                        Blog = Convert.ToString(reader["Blog"]),
                                        BlogType = Convert.ToString(reader["BlogType"]),
                                        FaceBook = Convert.ToString(reader["FaceBook"]),
                                        Twitter = Convert.ToString(reader["Twitter"]),
                                        YouTube = Convert.ToString(reader["YouTube"]),
                                        Pinterest = Convert.ToString(reader["Pinterest"]),
                                        Instagram = Convert.ToString(reader["Instagram"]),
                                        Wikipedia = Convert.ToString(reader["Wikipedia"]),
                                        ImgProfile = Convert.ToString(reader["ImgProfile"]),
                                        ImgComments = Convert.ToString(reader["ImgComments"]),
                                        DOB = reader["DOB"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["DOB"],
                                        DOD = reader["DOD"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["DOD"]
                                    },
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return authorInfo;
        }
    }
}
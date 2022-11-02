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
    public class LoginRepository
    {
        public int CheckUserExists(LoginModels mdlLogin)
        {
            int userId = 0;
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.CheckUserExists, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", mdlLogin.Name.Trim().ToLower());
                    cmd.Parameters.AddWithValue("@Password", Encryption.Encrypt(mdlLogin.Password.Trim()));
                    //cmd.Parameters.AddWithValue("@Password", mdlLogin.Password.Trim());

                    cmd.Parameters.Add("@Result", SqlDbType.Int);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        userId = DataConversion.Convert2Int32(cmd.Parameters["@Result"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }
            return userId;
        }

        public LoginModels UserLogin(string email, string password)
        {
            LoginModels mdlLogin = new LoginModels();
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetLogInUser, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@emailId", email.Trim().ToLower());
                    cmd.Parameters.AddWithValue("@password", Encryption.Encrypt(password.Trim()));
                    
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                mdlLogin = new LoginModels()
                                {
                                    LoginID = Convert.ToByte(reader["UserID"]),
                                    Name = Convert.ToString(reader["UserName"]),
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }
            return mdlLogin;
        }

        public int UpdateUserProfileByUserID(UserModels user)
        {
            int i = 0;
            using (SqlConnection sqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand com = new SqlCommand(SQLObjects.UpdateUserProfile, sqlConnection))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserID", user.UserID);
                    com.Parameters.AddWithValue("@UserName", user.UserName.Trim());
                    com.Parameters.AddWithValue("@Name", user.Name);
                    com.Parameters.AddWithValue("@EMail", user.Email == null ? "" : user.Email.ToLower().Replace(" ", "").Trim());
                    com.Parameters.AddWithValue("@Mobile", user.Mobile == null ? "" : user.Mobile.Trim());

                    try
                    {
                        sqlConnection.Open();
                        i = com.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            return i;
        }

        public LoginModels GetUserProfileByUserID(int userId)
        {
            LoginModels mdlLogin = new LoginModels();
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetUserProfileByUserID, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", userId);

                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                mdlLogin = new LoginModels()
                                {
                                    LoginID = Convert.ToByte(reader["UserID"]),
                                    Name = Convert.ToString(reader["UserName"]),
                                    Mobile = Convert.ToString(reader["Mobile"]),
                                    EMail = Convert.ToString(reader["MailID"]),
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }
            return mdlLogin;
        }
    }
}
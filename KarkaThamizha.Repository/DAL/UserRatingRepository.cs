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
    public class UserRatingRepository
    {
        public void AddOrUpdateUserRating(int userId, int bookId, int userRating)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddOrUpdateUserRating, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    cmd.Parameters.AddWithValue("@RatingStars", userRating);

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                }
            }
        }

        public UserRatingModels GetUserRatingByUserAndBookID(int userID, int bookID)
        {
            UserRatingModels ratingModels = new UserRatingModels();
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetUserRatingByUserAndBookID, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            ratingModels.UserID = int.Parse(reader["BookID"].ToString());
                            ratingModels.BookID = int.Parse(reader["BookID"].ToString());
                            ratingModels.UserRatingID = int.Parse(reader["UserRatingID"].ToString());
                            ratingModels.Rating = int.Parse(reader["Rating"].ToString());
                            ratingModels.Book = reader["Book"].ToString();
                            ratingModels.BookName = reader["Name"].ToString();
                            ratingModels.AvgUserRating = reader["AvgRating"].ToString();
                        }
                    }
                    return ratingModels;
                }
            }
        }
    }
}
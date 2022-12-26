using KarkaContract;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.SQLObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KarkaThamizha.Repository.DAL
{
    public class UserCommentsRepository : IUserCommentsRepository
    {
        public void AddOrUpdateUserCommentsByBookID(UserComments userComments)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                userComments.parent = userComments.parent == null ? "" : userComments.parent;
                using (SqlCommand cmd = new SqlCommand(SQLObjects.AddOrUpdateUserCommentsByBookID, con))
                {
                    cmd.Parameters.AddWithValue("@ID", userComments.id);
                    cmd.Parameters.AddWithValue("@BookID", userComments.BookId);
                    cmd.Parameters.AddWithValue("@AuthorID", userComments.AuthorId);
                    cmd.Parameters.AddWithValue("@ParentID", userComments.parent);
                    cmd.Parameters.AddWithValue("@CreatorID", userComments.creator);
                    cmd.Parameters.AddWithValue("@Content", userComments.content);
                    cmd.Parameters.AddWithValue("@Status", true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserComments(int commentID)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.DeleteUserCommentsByID, con))
                {
                    cmd.Parameters.AddWithValue("ID", commentID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                }
            }
        }

        public List<UserComments> GetUserCommentsByBookID(int bookID, int authorID, int userID)
        {
            List<UserComments> userComments = new List<UserComments>();
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.GetUserCommentsByBookID, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    cmd.Parameters.AddWithValue("@AuthorID", authorID);
                    cmd.Parameters.AddWithValue("@CurrentUserID", userID);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            UserComments comments = new UserComments();

                            comments.id = Convert.ToInt32(reader["ID"]);
                            if (string.IsNullOrEmpty(reader["ParentID"].ToString()))
                            {
                                comments.parent = null;
                            }
                            else
                            {
                                comments.parent = reader["ParentID"].ToString();
                            }
                             
                            comments.created = DateTime.Parse(reader["CreatedOn"].ToString()).ToString("MM/dd/yyyy");
                            comments.modified = DateTime.Parse(reader["ModifiedOn"].ToString()).ToString("MM/dd/yyyy");
                            comments.creator = Convert.ToInt32(reader["CreatorID"]);
                            comments.content = reader["Content"].ToString();
                            comments.fullname = reader["FullName"].ToString();
                            comments.profile_picture_url = reader["ProfilePicUrl"].ToString();
                            comments.created_by_admin = bool.Parse(reader["CreatedByAdmin"].ToString());
                            comments.created_by_current_user = bool.Parse(reader["CreatedByCurrentUser"].ToString());
                            comments.upvote_count = Convert.ToInt32(reader["UpVoteCount"]);
                            comments.user_has_upvoted = bool.Parse(reader["UserHasUpVoted"].ToString());
                            comments.is_new = bool.Parse(reader["IsNew"].ToString());

                            userComments.Add(comments);
                        }
                    }
                    return userComments;
                }
            }
        }

        public void UpVoteUserComments(int commentID,  int upVoteCount, bool isUpVote)
        {
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLObjects.UpVoteUserCommentsByID, con))
                {
                    cmd.Parameters.AddWithValue("ID", commentID); 
                    cmd.Parameters.AddWithValue("@UpVoteCount", upVoteCount);
                    cmd.Parameters.AddWithValue("@UserHasUpVoted", isUpVote); 
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
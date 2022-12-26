using KarkaThamizha.Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarkaContract
{
    public interface IUserCommentsRepository
    {
        List<UserComments> GetUserCommentsByBookID(int bookID, int authorID, int userID);
        void AddOrUpdateUserCommentsByBookID(UserComments userComments);
        void UpVoteUserComments(int commentID, int upVoteCount, bool isUpVote);
        void DeleteUserComments(int commentID);
    }
}

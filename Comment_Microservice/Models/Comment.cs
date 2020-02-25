using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Models
{
    public class Comment
    {
        #region Properties
        /// <summary>
        /// Must be unique identifier to that comment.
        /// Will be x characters long.
        /// </summary>
        public string CommentID { get; set; }

        /// <summary>
        /// Must be unique identifier for the post the comment belongs to.
        /// Will be x characters long.
        /// </summary>
        public string PostID { get; set; }

        /// <summary>
        /// One way identification of which comment this comment is replying to.
        /// Must be unique identifier to parent comment.
        /// Will be x characters long.
        /// </summary>
        public string ParentCommentID { get; set; }

        /// <summary>
        /// body of the comment.
        /// May include formatting, TBD
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Username of person who posted comment.
        /// Not using whole User Object to maintain security
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Time when comment was posted.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Shows how many times comment has been edited.
        /// Allows program to display old edits by matching CommentID and TimesEdited
        /// within the Event Queue.
        /// Set to 0 if comment has not been edited.
        /// </summary>
        public int TimesEdited { get; set; }

        #endregion
    }
}

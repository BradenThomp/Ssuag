using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.DataTransferObjects
{
    /// <summary>
    /// Tells microservice which comment to delete
    /// </summary>
    public class DeleteCommentDto
    {
        public Guid CommentId { get; set; }
    }
}

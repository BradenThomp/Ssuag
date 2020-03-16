using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.DataTransferObjects
{
    public class EditCommentDto
    {
        public Guid CommentId { get; set; }

        public string Content { get; set; }

    }
}

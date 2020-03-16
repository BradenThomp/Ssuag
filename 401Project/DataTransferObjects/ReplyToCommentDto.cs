using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.DataTransferObjects
{
    public class ReplyToCommentDto
    {
        public Guid ParentId { get; set; }

        public Guid PostId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }
    }
}

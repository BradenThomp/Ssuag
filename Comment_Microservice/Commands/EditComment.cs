using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands
{
    public class EditComment
    {
        public Guid CommentId { get; }

        public string Content { get; }
    }
}

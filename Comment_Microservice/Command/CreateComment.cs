using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Command
{
    public class CreateComment
    {

        public Guid CommentId { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime TimeOfCreation { get; set; }
    }
}

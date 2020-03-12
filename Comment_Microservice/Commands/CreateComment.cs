using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands
{
    /// <summary>
    /// Stores all contents of the new comment to be created.
    /// </summary>
    public class CreateComment
    {

        public Guid CommentId { get; set; }
        
        public Guid PostId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime TimeOfCreation { get; set; }
    }
}

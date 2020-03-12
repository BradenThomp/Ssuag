using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands
{
    public class CreateComment
    {

        public Guid CommentId { get; private set; }

        public Guid PostId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime TimeOfRecieval { get; private set; }

        public void generateId()
        {
            CommentId = Guid.NewGuid();
        }

        public void generateRecievalTime()
        {
            TimeOfRecieval = DateTime.UtcNow;
        }
    }
}

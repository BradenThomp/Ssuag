﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands
{
    /// <summary>
    /// Tells microservice to edit a specified comment, and what the new content should be.
    /// </summary>
    public class EditComment
    {
        public Guid CommentId { get; }

        public string Content { get; }
    }
}

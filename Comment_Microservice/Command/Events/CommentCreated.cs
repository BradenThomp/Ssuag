﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Command.Events
{
    public class CommentCreated
    {
        public CommentCreated(Guid commentId, int postId, string content, string username)
        {
            CommentId = commentId;
            PostId = postId;
            Content = content;
            Username = username;
        }

        public Guid CommentId { get; }

        public int PostId { get; }

        public string Content { get; }

        public string Username { get; }

    }
}

using _401Project.DataTransferObjects;
using _401Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.ViewModels
{
	public class PostInspectViewModel
	{
		public Post Post { get; set; }

		public string UserName { get; set; }
		public string newCommentContent { get; set; }
		
		public List<Comment> commentsExample {get; set;}
		
		public string CommentHTML;
		
		public PostInspectViewModel()
		{
			commentsExample = new List<Comment>();
			commentsExample.Add(new Comment{CommentId = 1, ParentId = null, Content="hello"});
			commentsExample.Add(new Comment{CommentId = 2, ParentId = 1, Content="there"});
			commentsExample.Add(new Comment{CommentId = 3, ParentId = null, Content="parent"});
			commentsExample.Add(new Comment{CommentId = 4, ParentId = 2, Content="general"});
			commentsExample.Add(new Comment{CommentId = 5, ParentId = 3, Content="child"});
			commentsExample.Add(new Comment{CommentId = 6, ParentId = 4, Content="kenobi"});
			
			CommentHTML = Comment.CommentListToHTML(commentsExample);

		}
    }
}

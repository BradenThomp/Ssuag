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
		/**
		 * Get Data
		 */
		public Post Post { get; set; }
		public List<Comment> Comments { get; set; }

		/**
		 * Post Data
		 */
		public Guid CommentRepliedToId { get; set; }
		public string CurrentUserName { get; set; }
		public string NewCommentContent { get; set; }
		
    }
}

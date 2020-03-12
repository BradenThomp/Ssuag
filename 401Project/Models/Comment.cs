using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.Models
{
    public class Comment
	{
		public int CommentId { get; set; }
		
		public Nullable<int> ParentId { get; set; }

		public string Content { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.Models
{
    public class Comment
	{
		public string Id { get; set; }
		
		public string ParentId { get; set; }

		public string PostId { get; set; }

		public string UserName { get; set; }

		public string Content { get; set; }
		
	}
}

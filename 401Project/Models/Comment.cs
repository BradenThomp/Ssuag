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
		
		public static string CommentListToHTML(List<Comment> CommentList)
		{
			string html = "";
			foreach(Comment comment in CommentList)
			{
				if(comment.ParentId == null)
				{
					html += ChildrenToHTML(CommentList, comment, 0);
				}
			}
			return html;
		}
		
		public static string ChildrenToHTML(List<Comment> CommentList, Comment parent, int depth)
		{
			string html = "";
			string offset = "";
			
			for (int i=0; i<depth; i++) offset += "-";
			html += "<p>" + offset + parent.Content + "</p>";
			foreach(Comment comment in CommentList)
			{
				if(comment.ParentId == parent.CommentId)
				{
					html += ChildrenToHTML(CommentList, comment, depth + 1);
				}
			}
			return html;
		}
	}
}

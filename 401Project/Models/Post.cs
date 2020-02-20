using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.Models
{
    public class Post
    {
        public int Id { get; set; }
        
        [Required]
        public string PhotoPath { get; set; }

        [Required]
        public string UserName { get; set; }

        public DateTime TimePosted { get; set; }
       
        public ICollection<Tag> Tags { get; set; }

    }
}

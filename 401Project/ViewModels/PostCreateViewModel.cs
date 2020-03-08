using _401Project.Models;
using _401Project.Helpers.DataValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.ViewModels
{
    public class PostCreateViewModel
    {

        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Photo { get; set; }

        public List<Tag> Tags { get; set; }

        public PostCreateViewModel()
        {
            Tags = new List<Tag>();
            for(int i = 0; i < 5; i++)
            {
                Tags.Add(new Tag());
            }
        }
    }
}

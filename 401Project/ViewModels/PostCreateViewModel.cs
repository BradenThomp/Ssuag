﻿using _401Project.Models;
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

        [Required]
        public IFormFile Photo { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _401Project.Models;
using _401Project.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using _401Project.Helpers.DataStructures;
using _401Project.Models.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _401Project.Controllers
{
    public class PostController : Controller
    {
        /// <summary>
        /// Reference to Post Storage -- This is where post data is stored
        /// </summary>
        private readonly IPostRepository PostRepository;

        /// <summary>
        /// Reference to the hosting environment instance added in the Startup.cs.
        /// </summary>
        private readonly IWebHostEnvironment HostingEnvironment;

        public PostController(IPostRepository postRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.PostRepository = postRepository;
            HostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Returns a paginated list view of posts. pageSize is the number of posts to include per page.
        /// </summary>
        public async Task<IActionResult> Browse(string sortOrder, string search, int? pageNumber)
        {
            var posts = PostRepository.ReadAllPosts();

            ViewData["TagFilter"] = search;

            if (!String.IsNullOrEmpty(search))
            {
                posts = posts.Where(s => s.Tags.Any(t => t.TagContent.Contains(search)));
                
            }

            switch (sortOrder)
            {
                case "date":
                    posts = posts.OrderBy(s => s.TimePosted);
                    break;
                case "date_desc":
                    posts = posts.OrderByDescending(s => s.TimePosted);
                    break;
                default:
                    posts = posts.OrderBy(s => s.TimePosted);
                    break;
            }

            int pageSize = 20;
            return View(await PaginatedList<Post>.CreateAsync(posts, pageNumber ?? 1, pageSize));
        }

        /// <summary>
        /// Returns an inspected view of a post where Id is the post to inspect -- This can also be refered to as the page that contains comments.
        /// </summary>
        public IActionResult Inspect(int Id)
        {
            PostInspectViewModel Model = new PostInspectViewModel
            {
                Post = PostRepository.ReadPost(Id)
            };
            return View(Model);
        }

        /// <summary>
        /// Returns the create page which contains a form to create a post.
        /// </summary>
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            PostCreateViewModel viewModel = new PostCreateViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// Called on submition of post create, Creates a new post from viewmodel and saves to repository.
        /// </summary>
        [Authorize]
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                string uniqueFileName = null;
                if(model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(HostingEnvironment.WebRootPath, "img/uploads");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                //Gets current user username
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var userName = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Name);

                Post newPost = new Post
                {
                    PhotoPath = uniqueFileName,
                    UserName = userName.ToString(),
                    TimePosted = DateTime.UtcNow,
                    Tags = new List<Tag>()
                };

                foreach(Tag t in model.Tags)
                {
                    if (t.TagContent != null)
                    {
                        newPost.Tags.Add(t);
                    }
                }

                PostRepository.Create(newPost);
                return RedirectToAction("inspect", new { id = newPost.Id });
            }

            return View();
        }
    }
}

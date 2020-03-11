using System;
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
        public async Task<IActionResult> Browse(int? pageNumber)
        {
            var posts = PostRepository.ReadAllPosts();

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
            return View();
        }

        // <summary>
        /// Called on submition of post create, Creates a new post from viewmodel and saves to repository.
        /// </summary>
        [Authorize]
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
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
                    Tags = null
                };

                PostRepository.Create(newPost);
                return RedirectToAction("inspect", new { id = newPost.Id });
            }

            return View();
        }
    }
}

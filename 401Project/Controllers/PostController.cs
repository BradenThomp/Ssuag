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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _401Project.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository PostRepository;

        private readonly IWebHostEnvironment HostingEnvironment;

        public PostController(IPostRepository postRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.PostRepository = postRepository;
            HostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inspect(int Id)
        {
            PostInspectViewModel Model = new PostInspectViewModel
            {
                Post = PostRepository.ReadPost(Id)
            };
            return View(Model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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

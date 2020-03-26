using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using _401Project.ViewModels;
using Comment_Microservice.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _401Project.Controllers
{
    public class CommentController : Controller
    {
        /// <summary>
        /// Takes user input from postnspectviewmodel to create a new CreateCommentDto which is then sent to the microservice
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment(PostInspectViewModel vm)
        {


            CreateCommentDto comment = new CreateCommentDto
            {
                Content = vm.NewCommentContent,
                PostId = vm.Post.Id,
                Username = vm.CurrentUserName
            };

            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:44332/api/command/createcomment", content))
                    {

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //TODO ADD ERROR HERE
                return RedirectToAction("inspect", "post", new { id = vm.Post.Id });
            }
            return RedirectToAction("inspect", "post", new { id = vm.Post.Id });
        }

        /// <summary>
        /// Takes user input from postnspectviewmodel to create a new ReplyToCommentDto which is then sent to the microservice
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReplyToComment(PostInspectViewModel vm)
        {
            ReplyToCommentDto comment = new ReplyToCommentDto
            {
                ParentId = vm.CommentRepliedToId,
                Content = vm.NewCommentContent,
                PostId = vm.Post.Id,
                Username = vm.CurrentUserName
            };

            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:44332/api/command/replytocomment", content))
                    {

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //TODO ADD ERROR HERE
                return RedirectToAction("inspect","post", new { id = vm.Post.Id });
            }
            return RedirectToAction("inspect", "post", new { id = vm.Post.Id });
        }

        /// <summary>
        /// Takes user input from postnspectviewmodel to create a new ReplyToCommentDto which is then sent to the microservice
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditComment(PostInspectViewModel vm)
        {
            EditCommentDto comment = new EditCommentDto
            {
                CommentId = vm.CommentEditedId,
                Content = vm.NewCommentContent
            };

            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:44332/api/command/editcomment", content))
                    {

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //TODO ADD ERROR HERE
                return RedirectToAction("inspect", "post", new { id = vm.Post.Id });
            }
            return RedirectToAction("inspect", "post", new { id = vm.Post.Id });
        }

        /// <summary>
        /// Takes user input from postnspectviewmodel to create a new ReplyToCommentDto which is then sent to the microservice
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid Id)
        {
            DeleteCommentDto comment = new DeleteCommentDto
            {
                CommentId = Id
            };

            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:44332/api/command/deletecomment", content))
                    {

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //TODO ADD ERROR HERE
                return RedirectToAction("browse", "post");
            }
            return RedirectToAction("browse", "post");
        }
    }
}

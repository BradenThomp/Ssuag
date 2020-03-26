using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comment_Microservice.Queries;
using Comment_Microservice.Queries.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comment_Microservice.Controllers
{
    [Route("api/[controller]/[action]")]
    public class QueryController : Controller
    {
        private readonly RavenDBRepository _repository;

        public QueryController(RavenDBRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public List<Comment> GetCommentsByPost(Guid PostId)
        {
            return _repository.GetCommentsByPost(PostId.ToString());
        }

    }
}

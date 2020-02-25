using _401Project.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.Models.Repository
{
    //Simple CRUD Repository Pattern - Google if confused
    public class SQLPostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public Post Create(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

        public Post Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> ReadAllPosts()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Post> ReadAllPostsPaginated()
        {
            var posts = from p in _context.Posts select p;
            return posts.AsNoTracking();
        }

        public Post ReadPost(int Id)
        {
            return _context.Posts.Find(Id);
        }

        public Post Update(Post changedPost)
        {
            throw new NotImplementedException();
        }
    }
}

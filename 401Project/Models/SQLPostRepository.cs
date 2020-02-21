using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.Models
{
    //Simple CRUD Repository Pattern - Google if confused
    public class SQLPostRepository : IPostRepository
    {
        private readonly ApplicationDbContext Context;

        public SQLPostRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        
        public Post Create(Post post)
        {
            Context.Posts.Add(post);
            Context.SaveChanges();
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

        public Post ReadPost(int Id)
        {
            return Context.Posts.Find(Id);
        }

        public Post Update(Post changedPost)
        {
            throw new NotImplementedException();
        }
    }
}

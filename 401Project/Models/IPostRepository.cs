using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _401Project.Models
{
    public interface IPostRepository
    {
        public Post Create(Post post);
        Post ReadPost(int Id);

        public IEnumerable<Post> ReadAllPosts();

        public Post Update(Post changedPost);

        public Post Delete(int Id);
    }
}

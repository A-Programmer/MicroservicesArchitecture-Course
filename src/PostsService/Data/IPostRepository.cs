using System.Collections.Generic;
using PostsService.Models;

namespace PostsService.Data
{
    public interface IPostRepository
    {
        bool SaveChanges();

        IEnumerable<Post> GetAllPosts();

        Post GetPostById(int id);

        void CreatePost(Post post);
    }
}
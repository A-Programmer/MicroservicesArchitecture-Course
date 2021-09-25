using System.Collections.Generic;
using System.Linq;
using PostsService.Models;

namespace PostsService.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.OrderByDescending(p => p.Id);
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
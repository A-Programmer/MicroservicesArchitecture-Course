using System;
using System.Collections.Generic;
using System.Linq;
using UsersService.Models;

namespace UsersService.Data
{
    public class UsersRepo : IUsersRepo
    {
        private readonly AppDbContext _context;

        public UsersRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePost(int userId, Post post)
        {
            if(post == null)
                throw new ArgumentNullException(nameof(post));
            post.UserId = userId;
            _context.Posts.Add(post);
        }

        public void CreateUser(User user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));
            _context.Users.Add(user);
        }

        public bool ExternalPostExists(int externalPostId)
        {
            return _context.Posts.Any(p => p.ExtenralId == externalPostId);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.OrderByDescending(x => x.Id).ToList();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Post GetPost(int userId, int postId)
        {
            return _context.Posts
                .Where(p => p.ExtenralId == postId && p.UserId == userId)
                .FirstOrDefault();
        }

        public IEnumerable<Post> GetUserPosts(int userId)
        {
            return _context.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Id);
        }

        public bool IsPostExist(int postId)
        {
            return _context.Posts.Any(p => p.ExtenralId == postId);
        }

        public bool IsUserExist(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
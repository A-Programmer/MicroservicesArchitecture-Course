using System;
using System.ComponentModel.DataAnnotations;

namespace UsersService.Models
{
    public class Post
    {
        public Post()
        {
            PublishedDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public int ExtenralId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime PublishedDate { get; private set; }
        public int UserId { get; set; }
        public User Owner { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace PostsService.Models
{
    public class Post
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.Now;
    }
}
using System.ComponentModel.DataAnnotations;

namespace PostsService.Dtos
{
    public class PostCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace UsersService.Dtos
{
    public class PostCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
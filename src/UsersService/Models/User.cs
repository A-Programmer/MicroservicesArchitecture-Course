using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsersService.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
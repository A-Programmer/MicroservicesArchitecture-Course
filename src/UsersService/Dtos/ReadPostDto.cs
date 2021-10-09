using System;

namespace UsersService.Dtos
{
    public class ReadPostDto
    {
        public int Id { get; set; }
        public int ExtenralId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
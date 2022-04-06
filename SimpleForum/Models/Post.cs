using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SimpleForum.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string PostDescription { get; set; }

        public DateTime DatePosted { get; set; }

        //foreignkeyuser
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public IdentityUser PostAuthor { get; set; }

        //onetomanythread
        public ForumThread Thread { get; set; }
    }
}

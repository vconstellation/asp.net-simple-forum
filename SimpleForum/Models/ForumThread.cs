using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SimpleForum.Models
{
    public class ForumThread
    {

        public int Id { get; set; }

        [Required]
        public string ThreadTitle { get; set; }


        //onetooneauthor
        [Required]
        public string UserId { get; set; }

        [Required]
        public IdentityUser ThreadAuthor { get; set; }

        //onetomanyposts
        public ICollection<Post>? Posts { get; set; }

        //onetomanycategories
        [Required]
        public Category Category { get; set; }
    }
}

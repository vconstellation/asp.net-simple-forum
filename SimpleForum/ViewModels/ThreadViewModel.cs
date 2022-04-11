using Microsoft.AspNetCore.Identity;
using SimpleForum.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpleForum.ViewModels
{
    public class ThreadViewModel
    {
        public int ThreadId { get; set; }

        public int PostId { get; set; }

        [Required]
        public string ThreadTitle { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public IdentityUser ThreadAuthor { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public string PostDescription { get; set; }

        public DateTime DatePosted { get; set; }

        public ForumThread Thread { get; set; }
    }
}

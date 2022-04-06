using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleForum.Data;
using SimpleForum.Models;

namespace SimpleForum.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {

        private readonly SimpleForumContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        
        public ForumController(SimpleForumContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        //CREATE CATEGORY
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            Console.WriteLine(category.Name);
            Console.WriteLine(category.Id);
            if (ModelState.IsValid)
            {
                Console.WriteLine("check if modelstate");
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            foreach (var item in errors)
            {
                Console.WriteLine(item.ErrorMessage);
            }
            Console.WriteLine("return to category");
            return View(category);
        }

        public async Task<IActionResult> ThreadList(int? id)
        {
            var category = await _context.Categories.FindAsync(id);

            ViewData["catId"] = id;
            Console.WriteLine("THIS IS ID FROM GET = " + id);

            Console.WriteLine(category.ToString);

            if (category == null)
            {
                return View();
            }

            var model = await _context.Threads.Where(n => n.Category == category).ToListAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateThread(int id, ForumThread thread)
        {


            ForumThread newThread = new ForumThread
            {
                ThreadTitle = thread.ThreadTitle,
                UserId = _userManager.GetUserId(User),
                ThreadAuthor = await _userManager.GetUserAsync(User),
                Category = await _context.Categories.FindAsync(id),
            };
            //thread.UserId = _userManager.GetUserId(User);
            //thread.ThreadAuthor = await _userManager.GetUserAsync(User);
            //thread.Category = await _context.Categories.FindAsync(1);

            Console.WriteLine(id);
            Console.WriteLine("THIS IS AN ID !!");
            _context.Add(newThread);
            await _context.SaveChangesAsync();

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (errors.Any())
            {
                foreach (var item in errors)
                {
                    Console.WriteLine(item.ErrorMessage);
                }
            }
 

            return View(thread);
        }

        public IActionResult CreateThread()
        {
            return View();
        }

        //posts
        public async Task<IActionResult> PostList(int? id)
        {
            var thread = await _context.Threads.FindAsync(id);

            ViewData["threadId"] = id;

            if (thread == null)
            {
                return NotFound();
            }

            var model = await _context.Posts.Where(n => n.Thread == thread).ToListAsync();

            return View(model);
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(int id, Post post)
        {
            Post newPost = new Post
            {
                PostDescription = post.PostDescription,
                DatePosted = DateTime.Now,
                UserId = _userManager.GetUserId(User),
                PostAuthor = await _userManager.GetUserAsync(User),
                Thread = await _context.Threads.FindAsync(id),
            };

            _context.Add(newPost);
            await _context.SaveChangesAsync();

            return View(post);
        }

        public async Task<IActionResult> EditPost(int id)
        {

            var currentUser = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);


            if (post == null)
            {
                return NotFound();
            }

            if (post.PostAuthor != currentUser)
            {
                return BadRequest();
            }

            var model = new Post
            {
                Id = post.Id,
                PostAuthor = post.PostAuthor,
                UserId = post.UserId,
                PostDescription = post.PostDescription,
                Thread = post.Thread,
                DatePosted = post.DatePosted
            };





            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Post model)
        {

            Console.WriteLine("Before the ModelState");

            var post = await _context.Posts.FindAsync(model.Id);

            if (post == null)
            {
                return NotFound();
            }

            post.PostDescription = model.PostDescription;

            _context.Update(post);
            await _context.SaveChangesAsync();


            Console.WriteLine("Outside of the ModelState");

            return View(post);
          
        }
        
    }
}

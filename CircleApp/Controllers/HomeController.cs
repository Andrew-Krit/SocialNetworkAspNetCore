using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetworkAspNetCore.Data;
using SocialNetworkAspNetCore.ViewModel.Home;
using SocialNetworkAspNetCore.Data.Models;
using System.Diagnostics;

namespace SocialNetworkAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _context = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var allPosts = await _context.Posts
                .Include(n => n.User)
                .ToListAsync();

            return View(allPosts);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePost(PostVM post)
        {
            var loggedInUser = 1;

            var newPost = new Post
            {
                Content = post.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                ImageUrl = "",
                NrOfReports = 0,
                UserId = loggedInUser
            };

            if(post.Image != null && post.Image.Length > 0)
            {
                var rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                if(post.Image.ContentType.Contains("image"))
                {
                    var rootFolderPathImages = Path.Combine(rootFolderPath, "images");
                    Directory.CreateDirectory(rootFolderPathImages);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(post.Image.FileName);
                    var filePath = Path.Combine(rootFolderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await post.Image.CopyToAsync(stream);


                    newPost.ImageUrl = "/images/" + fileName;
;                }
            }

            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetworkAspNetCore.Data;
using SocialNetworkAspNetCore.ViewModel.Home;
using SocialNetworkAspNetCore.Data.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SocialNetworkAspNetCore.Data.Services;
using SocialNetworkAspNetCore.Data.Helpers;

namespace SocialNetworkAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IPostsService _postsService;
        private readonly IFilesService _filesService;
        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext, IPostsService postsService, IFilesService filesService)
        {
            _logger = logger;
            _context = appDbContext;
            _postsService = postsService;
            _filesService = filesService;
        }

        public async Task<IActionResult> Index()
        {
            var allPosts = await _postsService.GetAllPostsAsync();

            return View(allPosts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostVM post)
        {
            var loggedInUser = 1;
            var imageUploadPath = await _filesService.UploadImageAsync(post.Image, ImageFileType.PostImage);

            var newPost = new Post
            {
                Content = post.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                ImageUrl = imageUploadPath,
                NrOfReports = 0,
                UserId = loggedInUser
            };

            await _postsService.CreatePostAsync(newPost);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> TogglePoskLike(PostLikeVM postLikeVM)
        {
            var loggedInUserId = 1;

            await _postsService.TogglePostLikeAsync(postLikeVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostComment(PostCommentVM postCommentVM)
        {
            var loggedInUserId = 1;

            var newComment = new Comment()
            {
                UserId = loggedInUserId,
                PostId = postCommentVM.PostId,
                Content = postCommentVM.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            await _postsService.AddPostCommentAsync(newComment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePostComment(RemoveCommentVM removeCommentVM)
        {
            await _postsService.RemovePostCommentAsync(removeCommentVM.CommentId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PostDelete(PostRemoveVM postRemoveVM)
        {
            await _postsService.RemovePostAsync(postRemoveVM.PostId);

            return RedirectToAction("Index");
        }
    }
}

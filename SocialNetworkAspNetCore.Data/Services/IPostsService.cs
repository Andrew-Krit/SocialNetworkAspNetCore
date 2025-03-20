using SocialNetworkAspNetCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocialNetworkAspNetCore.Data.Services
{
    public interface IPostsService
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> CreatePostAsync(Post post);
        Task RemovePostAsync(int postId);

        Task AddPostCommentAsync(Comment comment);
        Task RemovePostCommentAsync(int commentId);

        Task TogglePostLikeAsync(int postId, int userId);

    }
}

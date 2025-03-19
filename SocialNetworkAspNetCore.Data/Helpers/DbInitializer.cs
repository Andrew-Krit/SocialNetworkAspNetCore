using SocialNetworkAspNetCore.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAspNetCore.Data.Helpers
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext appDbContext)
        {
            if (!appDbContext.Users.Any() && !appDbContext.Posts.Any())
            {
                var newUser = new User()
                {
                    FullName = "Andrew Krit",
                    ProfilePictureUrl = "https://i.pinimg.com/736x/0b/c3/74/0bc374671f4fefb65b5168c41e906bbf.jpg"
                };
                await appDbContext.Users.AddAsync(newUser);
                await appDbContext.SaveChangesAsync();

                var newPostWithoutImage = new Post()
                {
                    Content = "Hello my name is Andrew Sicknezz! Hello, World!",
                    ImageUrl = "",
                    NrOfReports = 0,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,

                    UserId = newUser.Id
                };

                var newPostWithImage = new Post()
                {
                    Content = "Hello my name is Andrew Sicknezz! Hello, World of Horror!",
                    ImageUrl = "https://i.pinimg.com/736x/d9/6d/c1/d96dc13cad3821f924a099ed6478ced8.jpg",
                    NrOfReports = 0,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,

                    UserId = newUser.Id
                };

                await appDbContext.Posts.AddRangeAsync(newPostWithoutImage, newPostWithImage);
                await appDbContext.SaveChangesAsync();
            }
        }

    }
}

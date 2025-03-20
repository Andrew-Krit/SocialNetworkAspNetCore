using SocialNetworkAspNetCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAspNetCore.Data.Services
{
    public interface IUsersService
    {
        Task<User> GetUser(int loggedInUserId);
    }
}

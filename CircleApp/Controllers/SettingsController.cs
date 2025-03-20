using Microsoft.AspNetCore.Mvc;
using SocialNetworkAspNetCore.Data.Services;

namespace SocialNetworkAspNetCore.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IUsersService _usersService;
        public SettingsController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            var loggedInUserId = 1;
            var userDb = await _usersService.GetUser(loggedInUserId);
            return View(userDb);
        }
    }
}

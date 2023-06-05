using IdentityExample.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        //add usermanager
        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index() { return View(); }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        //-----------------------------------------------------------

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                //sign in 
                var signinResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                    
                if(signinResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                    
            }

            return RedirectToAction("Index");
        }

        //-----------------------------------------------------------
        public IActionResult Register()
        { return View(); }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = "",
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var signinResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signinResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

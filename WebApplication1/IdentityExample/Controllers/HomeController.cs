using IdentityExample.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System.Security.Claims;


namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        //add usermanager
        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,IEmailService emailService)
        {
            
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }
        public IActionResult Index() { return View(); }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        //-----------------------------------------------------------
        #region Login 
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
        #endregion
        //----------------------------------------------REGISTER------------------------------------------------------------------------
        #region Registration
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
               // var signinResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

               
                    //Initiate the email registration and confirmation -- generate EMAIL/Confirmation token
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //sets the path URL and the action = VerifyEmail taken , with the appropriate value of user.Id
                    var link = Url.Action(nameof(VerifyEmail),"Home",new { userID = user.Id, code }, Request.Scheme,Request.Host.ToString() );

                    await _emailService.SendAsync("test@test.com","email verify",$"<a href=\"{link}\"> Verify Email </a>",true);

                    return RedirectToAction("EmailVerification"); //redirects to the email verification page 
                
            }
            return RedirectToAction("Index");
        }
        #endregion
        //----------------------------------------------Email Verification View-------------------------------------------------------
        #region Email Verification
        public IActionResult EmailVerification() => View();

        //----------------------------------------------LogOut------------------------------------------------------------------------
        public async Task<IActionResult> VerifyEmail(string userId, string code) //the email confirmation token is required for the verification from above
        {
            //Finds the user and confirms if he exists/has the correct Email Token 
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return BadRequest(); }
            //token validation for specific user
            var result = await _userManager.ConfirmEmailAsync(user,code);
            if (result.Succeeded) { return View(); }

            return View();
        }
        #endregion
        //----------------------------------------------LogOut------------------------------------------------------------------------
        #region Log out
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}

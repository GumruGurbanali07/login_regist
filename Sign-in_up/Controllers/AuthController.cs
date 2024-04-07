using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Sign_in_up.DTOs;
using Sign_in_up.Models;

namespace Sign_in_up.Controllers
{
	public class AuthController : Controller
	{
		readonly private UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginDTO loginDTO)
		{
			var findEmail = await _userManager.FindByEmailAsync(loginDTO.Email);
			if (findEmail == null)
			{
				return RedirectToAction("Login");
			}
			Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(findEmail, loginDTO.Password, false, false);
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			}
			return View(loginDTO);
		}

		[HttpGet]
		public IActionResult Register()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
		[HttpPost]
		
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		[HttpPost]
		
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDTO);
            }

            var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email already exists!");
                return View(registerDTO);
            }

            var newUser = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                UserName = registerDTO.Email,
                Email = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
            {
                // User created successfully, now sign in the user
                await _signInManager.SignInAsync(newUser, isPersistent: false); // You can set isPersistent to true if you want persistent cookies

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Failed to create the user, add model errors based on IdentityResult errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(registerDTO);
            }
        }
	}
}
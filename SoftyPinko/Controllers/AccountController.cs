using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftyPinko.Helper;
using SoftyPinko.Models;
using SoftyPinko.ViewModels;
using System.Threading.Tasks;

namespace SoftyPinko.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new()
            {
                Name = registerVM.Name,
                UserName = registerVM.UserName,
                Email = registerVM.EmailAdress,

            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, RoleEnum.Admin.ToString());

            return RedirectToAction("login");

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmailAddress);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmailAddress);
                if (user is null)
                {
                    ModelState.AddModelError("", "Email ve ya UserName sehvdir");
                }

            }

            var result =await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, false);

          

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut(int id)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach (var roles in Enum.GetValues(typeof(RoleEnum)))
            {
               await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = roles.ToString()
                });
            }
            return Content("Rollar Yaradildi");
        }

    }
}

using MediPlusMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediPlusMVC.DAL.Contexts;
using MediPlusMVC.DTO.UserDTO;

namespace FrontToBack.Controllers
{
    public class AccountsController : Controller
    {
        readonly AppDBContext _context;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        public AccountsController(AppDBContext appDBContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = appDBContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createUserDto);
            }

            AppUser appUser = new AppUser();
            appUser.FirstName = createUserDto.FirstName;
            appUser.UserName = createUserDto.UserName;
            appUser.LastName = createUserDto.LastName;
            appUser.Email = createUserDto.Email;
            var result = await _userManager.CreateAsync(appUser, createUserDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View(createUserDto);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "Something went wrong.");
            //    return View();
            //}
            AppUser? user = (AppUser?)_context.Users.FirstOrDefault(u => u.Email == loginUserDto.EmailOrUserName || u.UserName == loginUserDto.EmailOrUserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect.");
                return View();
            }
            await _signInManager.SignInAsync(user, isPersistent: true);
            return RedirectToAction("Index", "Home");
        }

        public async  Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

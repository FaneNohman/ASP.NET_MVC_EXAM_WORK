using CORE_MVC_EXAM.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace CORE_MVC_EXAM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            var form = HttpContext.Request.Form;
            if (!form.ContainsKey("login") || !form.ContainsKey("password"))
                return BadRequest("Email и/или пароль не установлены");
            string login = form["login"];
            string password = form["password"];

            User? person = _dbContext.Users.FirstOrDefault(p => p.Login == login && p.Password == password);
            if (person is null) return Unauthorized();
            Role? role = _dbContext.Roles.FirstOrDefault(p => p.RoleId == person.RoleId);
            if (role is null) return Unauthorized();

            await HttpContext.SignInAsync(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new List<Claim>
                                        {
                                        new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserId.ToString()),
                                        new Claim(ClaimsIdentity.DefaultRoleClaimType,role.RoleName )
                                        }, 
                        "Cookies")));
            return Redirect(returnUrl ?? "/");
        }

        [Authorize(Roles = "admin, teacher, student")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin, teacher, student")]
        public IActionResult Profile()
        {
            User? user = _dbContext.Users.FirstOrDefault(p => p.UserId.ToString() == User.Identity.Name);
            if (user is null) return Redirect("Index");
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationApp1.Data;
using UserRegistrationApp1.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace UserRegistrationApp1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private const string AllowedEmailDomain = "@gmail.com";
        private const int PageSize = 5;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        // GET: Register
        public IActionResult Register()
        {
            ViewBag.HideLayout = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            ViewBag.HideLayout = true;

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid submission.");
                return View(user);
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                ModelState.AddModelError("Email", "Email is mandatory.");
            }
            else if (!user.Email.EndsWith(AllowedEmailDomain, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Email", $"Email must end with {AllowedEmailDomain}");
            }
            else if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
            }

            if (user.DOB == default)
            {
                ModelState.AddModelError("DOB", "Date of Birth is mandatory.");
            }
            else
            {
                int age = DateTime.Today.Year - user.DOB.Year;
                if (user.DOB > DateTime.Today.AddYears(-age)) age--;

                if (age < 18)
                    ModelState.AddModelError("DOB", "You must be at least 18 years old.");
            }

            if (ModelState.IsValid)
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
                user.IsDisabled = false;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Login
        public IActionResult Login()
        {
            ViewBag.HideLayout = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            ViewBag.HideLayout = true;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                TempData["Toasts"] = "User does not exist.";
                return View("Login");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                TempData["Toasts"] = "Invalid email or password.";
                return View("Login");
            }

            if (user.IsDisabled)
            {
                TempData["Toasts"] = "Your account has been disabled.";
                return View("Login");
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);

            TempData["SuccessMessage"] = "Login successful!";
            return RedirectToAction("Index", "Account");
        }

        // GET: Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: User List
        public IActionResult Index(string search, int? page)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                users = users.Where(u =>
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search) ||
                    u.Address.ToLower().Contains(search));
            }

            var pageNumber = page ?? 1;
            var pagedUsers = users.OrderBy(u => u.Id).ToPagedList(pageNumber, PageSize);

            ViewBag.Search = search;
            return View(pagedUsers);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User updated successfully.";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // POST: Toggle Status
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return Json(new { success = false, message = "User not found." });

            user.IsDisabled = !user.IsDisabled;
            await _context.SaveChangesAsync();

            return Json(new { success = true, isDisabled = user.IsDisabled });
        }

        // POST: Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "User deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}

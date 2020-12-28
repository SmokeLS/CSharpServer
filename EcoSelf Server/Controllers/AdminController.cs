using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using EcoSelf_Server.ViewModels;
using EcoSelf_Server.Models;
using Microsoft.AspNetCore.Routing;

namespace EcoSelf_Server.Controllers
{
    public class AdminController : Controller
    {
        private ServerDBContex db;
        public AdminController(ServerDBContex context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return Redirect("/Admin/index");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await db.Products.ToListAsync());
            }
            return Content("Неправильный логин или пароль");
        }
        public IActionResult Add()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return Content("Войдите в систему");
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return View();
        }
        public async Task<IActionResult> EditAsync(int? Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Id != null)
                {
                    Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == Id);
                    if (product != null)
                        return View(product);
                }
                return NotFound();
            }
            return Content("Войдите в систему");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (User.Identity.IsAuthenticated)
            {
                db.Products.Update(product);
                await db.SaveChangesAsync();
                return RedirectToAction("edit");
            }
            return Content("Войдите в систему");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != null)
                {
                    Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                    if (product != null)
                        return View(product);
                }
                return NotFound();
            }
            return Content("Войдите в систему");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != null)
                {
                    Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                    if (product != null)
                    {
                        db.Products.Remove(product);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                return NotFound();
            }
            return Content("Войдите в систему");
        }
        /*   public async Task<IActionResult> Add(AddModel model)
           {
               if (ModelState.IsValid)
               {
                   User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                   if (user == null)
                   {
                       // добавляем пользователя в бд
                       db.Users.Add(new User { Email = model.Email, Password = model.Password });
                       await db.SaveChangesAsync();

                       await Authenticate(model.Email); // аутентификация

                       return RedirectToAction("Index", "Scanner");
                   }
                   else
                       ModelState.AddModelError("", "Некорректные логин и(или) пароль");
               }
               return View(model);
           }*/

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        /* public async Task<IActionResult> Logout()
         {
             await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
             return RedirectToAction("Login", "Scanner");
         }*/

   
    }
}
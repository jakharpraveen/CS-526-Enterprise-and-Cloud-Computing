using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ImageSharingWithSecurity.DAL;
using ImageSharingWithSecurity.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ImageSharingWithSecurity.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        protected SignInManager<ApplicationUser> signInManager;

        private readonly ILogger<AccountController> logger;

        // Dependency injection of DB context and user/signin managers
        public AccountController(UserManager<ApplicationUser> userManager, 
                                 SignInManager<ApplicationUser> signInManager, 
                                 ApplicationDbContext db,
                                 ILogger<AccountController> logger) 
            : base(userManager, db)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [HttpGet]
        // TODO security annotations
        [AllowAnonymous]
        public ActionResult Register()
        {
            CheckAda();
            return View();
        }

        [HttpPost]
        // TODO security annotations
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            CheckAda();

            if (ModelState.IsValid)
            {
                logger.LogDebug("Registering user: " + model.Email);
                IdentityResult result = null;
                // TODO register the user from the model, and log them in

                var user = new ApplicationUser(model.Email, model.ADA);

                result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    SaveADACookie(model.ADA);
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }

        [HttpGet]
        // TODO security annotations
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            CheckAda();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        // TODO security annotations
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            CheckAda();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO log in the user from the model (make sure they are still active)

            var user = await userManager.FindByNameAsync(model.UserName);
            if(user != null){
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (user.Active)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed");
                }
            }

            return View(model);
        }

        //
        // GET: /Account/Password

       [HttpGet]
        public ActionResult Password(PasswordMessageId? message)
        {
            CheckAda();
            ViewBag.StatusMessage =
                 message == PasswordMessageId.ChangePasswordSuccess ? "Your password has been changed."
                 : message == PasswordMessageId.SetPasswordSuccess ? "Your password has been set."
                 : message == PasswordMessageId.RemoveLoginSuccess ? "The external login was removed."
                 : "";
            ViewBag.ReturnUrl = Url.Action("Password");
            return View();
        }

        //
        // POST: /Account/Password

        [HttpPost]
        [ValidateAntiForgeryToken]
        // TODO security annotations
        public async Task<ActionResult> Password(LocalPasswordModel model)
        {
            CheckAda();

            ViewBag.ReturnUrl = Url.Action("Password");
            if (ModelState.IsValid)
            {
                IdentityResult idResult = null; ;

                // TODO change the password
                ApplicationUser user = await GetLoggedInUser();

                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                idResult = await userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        // TODO security
        [Authorize(Roles = "Admin")]
        public IActionResult Manage()
        {
            CheckAda();

            List<SelectListItem> users = new List<SelectListItem>();
            foreach (var u in db.Users)
            {
                SelectListItem item = new SelectListItem { Text = u.UserName, Value = u.Id, Selected = u.Active };
                users.Add(item);
            }

            ViewBag.message = "";
            ManageModel model = new ManageModel { Users = users };
            return View(model);
        }

        [HttpPost]
        // TODO security annotations
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(ManageModel model)
        {
            CheckAda();

            foreach (var userItem in model.Users)
            {
                ApplicationUser user = await userManager.FindByIdAsync(userItem.Value);

                // Need to reset user name in view model before returning to user, it is not posted back
                userItem.Text = user.UserName;

                if (user.Active && !userItem.Selected)
                {
                    var images = db.Entry(user).Collection(u => u.Images).Query().ToList();
                    foreach (Image image in images)
                    {
                        db.Images.Remove(image);
                    }
                    user.Active = false;
                }
                else if (!user.Active && userItem.Selected)
                {
                    /*
                     * Reactivate a user
                     */
                    user.Active = true;
                }
            }
            await db.SaveChangesAsync();

            ViewBag.message = "Users successfully deactivated/reactivated";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logoff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        protected void SaveADACookie(bool value)
        {
            // TODO save the value in a cookie field key
            var options = new CookieOptions() { IsEssential = true, Expires = DateTime.Now.AddMinutes(5), Secure = true };            
            Response.Cookies.Append("ADA", value.ToString(), options);

        }

        public enum PasswordMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

    }
}

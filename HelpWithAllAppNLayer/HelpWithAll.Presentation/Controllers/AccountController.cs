using AutoMapper;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using HelpWithAll.Presentation.ViewModel;
using System.Data;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using HelpWithAll.Infrastructure.Context;

namespace HelpWithAll.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    
        
        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var user = new UserViewModel()
            {
                Email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value!,
                Login = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value!,
                Roles = User.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList(),
            

            };
            return View(user);
        
        }
    

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(Index));
            }
            return base.View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserViewModel newUser)
        {
            var user = new IdentityUser{
                UserName = newUser.Login,
                Email = newUser.Email
            };
            var result = await userManager.CreateAsync(user, newUser.Password);
            if(!result.Succeeded){
                ModelState.AddModelError("All", result.Errors.FirstOrDefault().Description);
                return View(newUser);
            }
            var foundUser = await this.userManager.FindByEmailAsync(user.Email);
            
            // userManager.AddToRolesAsync(foundUser,"User");
        
            var signInResult = await signInManager.PasswordSignInAsync(foundUser, newUser.Password, true, true);
            return base.RedirectToAction(actionName: nameof(Index), controllerName: "Home");
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(Index));
            }
            if (string.IsNullOrWhiteSpace(returnUrl) == false)
            {
                ViewData["returnUrl"] = returnUrl;
            }
            return base.View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel userModel)
        {
            
            var foundUser = await this.userManager.FindByEmailAsync(userModel.Email);
            
            
        
            var signInResult = await signInManager.PasswordSignInAsync(foundUser, userModel.Password, true, true);
            if(!signInResult.Succeeded){
                return View(userModel);
            }
            return base.RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return base.RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        

        // [HttpGet]
        // public async Task<IActionResult> AddRoles(){
        //     await roleManager.CreateAsync(new IdentityRole("User"));
        //     await roleManager.CreateAsync(new IdentityRole("Admin"));
        //     var email = "admin@admin";
        //         var result = await userManager.FindByEmailAsync(email);
        //         if (result == null)
        //         {
        //             var admin = new IdentityUser { UserName = "admin", Email = email };
        //             await userManager.CreateAsync(admin, "Admin123!");
        //             await userManager.AddToRoleAsync(admin, "Admin");
        //         }
        //     return base.Ok();
        // }
    }
}
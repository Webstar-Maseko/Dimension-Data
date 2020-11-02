using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dimension_Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components;

namespace Dimension_Data.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly  SignInManager<IdentityUser>_signInManager;
       //private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn)
        {
            _logger = logger;
            _signInManager = signIn;
           // _userManager = user;
        }
        
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                
                return LocalRedirect("/Employee/");
            }
            else
            {
                return LocalRedirect("/Identity/Account/Login");
            }
            
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

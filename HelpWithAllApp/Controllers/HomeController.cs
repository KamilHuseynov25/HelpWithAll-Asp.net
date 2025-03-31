using System.Diagnostics;
using HelpWithAllApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpWithAllApp.Controllers;

public class HomeController : Controller
{




    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }


}

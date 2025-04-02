using System.Diagnostics;
using HelpWithAll.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpWithAll.Presentation.Controllers;

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

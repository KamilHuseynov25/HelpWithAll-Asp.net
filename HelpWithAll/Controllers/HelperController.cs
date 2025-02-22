
using HelpWithAll.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpWithAll.Controllers;
public class HelperController : ControllerBase
{
    [HttpGet("{id}")]
    public Helper GetUser(int id) {
        return new Helper {
            Name = "Kamil",
            Surname = "Huseynov",
            PaymentPerHour = 34
        };
    }
}

using System.Net;
using HelpWithAllApp.Models;
using HelpWithAllApp.Models.Response;
using HelpWithAllApp.Repositories.Base;
using HelpWithAllApp.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelpWithAllApp.Controllers;

[Route("[controller]/[action]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.OK)]
public class CustomerController : Controller
{
    private readonly ICustomerRepository customerRepository;
    private readonly IHttpLogger logger;


    public CustomerController(ICustomerRepository customerRepository, IHttpLogger logger)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;

    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CreateCustomer()
    {
        return View();
    }

    [HttpPost]
    [ActionName("CreateCustomer")]
    public async Task<IActionResult> CreateCustomer(Customer customer)
    {
        var checker = await customerRepository.InsertCustomerAsync(customer);
        if(checker){return base.RedirectToAction("Index");}
        else{
            return StatusCode(500, "An error occurred while deleting the customer.");
        }
    }

    [HttpGet]
    [ActionName("ShowAllCustomers")]
    public async Task<IActionResult> ShowAllCustomersAsync()
    {
        var customers = await customerRepository.SelectAllCustomersAsync();
        return base.Ok(customers);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCustomer(int id)
    {
        var customer = await customerRepository.SelectCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return View(customer);
    }

    [HttpPost]
    [ActionName("UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer(Customer newCustomer)
    {
        if (!ModelState.IsValid)
        {
            return View(newCustomer);
        }

        var checker = await customerRepository.UpdateCustomerAsync(newCustomer);
        if (checker)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Error updating customer. Please try again.");
            return View(newCustomer);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var checker = await customerRepository.DeleteCustomerAsync(id);
        if (checker)
        {
            return base.Ok();
        }
        else
        {
            return StatusCode(500, "An error occurred while deleting the customer.");
        }
    }
}

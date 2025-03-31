using HelpWithAllApp.Models;
using HelpWithAllApp.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelpWithAllApp.Controllers;

[Route("[controller]/[action]")]
public class CustomerController : Controller
{
    private readonly ICustomerRepository customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
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
        await customerRepository.InsertCustomerAsync(customer);
        return base.RedirectToAction("Index");
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

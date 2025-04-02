
using System.Net;

using HelpWithAll.Core.Models.Response;
using HelpWithAll.Core.Models;
using HelpWithAll.Core.Validators;
using HelpWithAll.Core.Options;
using HelpWithAll.Core.Repositories.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using HelpWithAll.Core.Service.Base;

namespace HelpWithAll.PresentationControllers;
[Route("[controller]/[action]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.OK)]
public class CustomerController : Controller
{
    private readonly ICustomerRepository customerRepository;
    private readonly IHttpLogger logger;
    private readonly IValidator<Customer> customerValidator;

    public CustomerController(ICustomerRepository customerRepository, IHttpLogger logger, IValidator<Customer> customerValidator)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;
        this.customerValidator = customerValidator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CreateCustomer()
    {
        if (TempData.TryGetValue("validation_errors", out object? validationErrorsObject))
        {
            if (validationErrorsObject is string validationErrorsJson)
            {
                var validationErrors = JsonSerializer.Deserialize<IEnumerable<ValidationErrorModel>>(validationErrorsJson);
                ViewData["validation_errors"] = validationErrors;
            }
        }
        return View();
    }

    [HttpPost]
    [ActionName("CreateCustomer")]
    public async Task<IActionResult> CreateCustomer(Customer customer)
    {
        var validationResult = await customerValidator.ValidateAsync(customer);

        if (!validationResult.IsValid)
        {
            TempData["validation_errors"] = JsonSerializer.Serialize(validationResult.Errors.Select(error => new ValidationErrorModel
            {
                Message = error.ErrorMessage,
                Property = error.PropertyName
            }));

            return RedirectToAction(nameof(CreateCustomer), "Customer");
        }

        return RedirectToAction(nameof(Index), "Customer");
    }

    [HttpGet]
    [ActionName("ShowAllCustomers")]
    public async Task<IActionResult> ShowAllCustomersAsync()
    {
        var customers = await customerRepository.SelectAllCustomersAsync();
        return Ok(customers);
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
        var validationResult = await customerValidator.ValidateAsync(newCustomer);

        if (!validationResult.IsValid)
        {
            TempData["validation_errors"] = validationResult.Errors.Select(error => new ValidationErrorModel
            {
                Message = error.ErrorMessage,
                Property = error.PropertyName
            });

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
            return Ok();
        }
        else
        {
            return StatusCode(500, "An error occurred while deleting the customer.");
        }
    }
}

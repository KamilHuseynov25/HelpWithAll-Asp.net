using System.Net;
using HelpWithAll.Core.Models;
using HelpWithAll.Core.Models.Response;
using HelpWithAll.Core.Validators;
using HelpWithAll.Core.Repositories.Base;
using HelpWithAll.Core.Service.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HelpWithAll.PresentationControllers;

[Route("[controller]/[action]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.OK)]
public class HelperController : Controller
{
    private readonly IHelperRepository helperRepository;
    private readonly IHttpLogger logger;
    private readonly IValidator<Helper> helperValidator;

    public HelperController(IHelperRepository helperRepository, IHttpLogger logger, IValidator<Helper> helperValidator)
    {
        this.helperRepository = helperRepository;
        this.logger = logger;
        this.helperValidator = helperValidator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CreateHelper()
    {
        if(base.TempData.TryGetValue("validation_errors", out object? validationErrorsObject)) {
            if(validationErrorsObject is string validationErrorsJson) {
                var validationErrors = JsonSerializer.Deserialize<IEnumerable<ValidationErrorModel>>(validationErrorsJson);
                
            
            }
            base.TempData.Keep("validation_errors");
        }

        return base.View();
    }

    [HttpPost]
[ActionName("CreateHelper")]
public async Task<IActionResult> CreateHelper(Helper helper)
{
    try
    {
        var validationResult = await helperValidator.ValidateAsync(helper);

        if (validationResult.IsValid==false)
        {
            base.TempData["validation_errors"] = JsonSerializer.Serialize(validationResult.Errors.Select(error => new ValidationErrorModel
            {
                Message = error.ErrorMessage,
                Property = error.PropertyName
            }));

            

            return base.RedirectToAction(nameof(CreateHelper), "Helper");
        }
        else
        {
            var checker = await helperRepository.InsertHelperAsync(helper);
            if (checker)
            {
                return RedirectToAction("Index", "Helper");
            }
            else
            {
                return StatusCode(500, "An error occurred while inserting the helper.");
            }
        }
    }
    catch (Exception ex)
    {
        
        return StatusCode(500, "An unexpected error occurred.");
    }
}


    [HttpGet]
    [ActionName("ShowAllHelpers")]
    public async Task<IActionResult> ShowAllHelpersAsync()
    {
        var helpers = await helperRepository.SelectAllHelpersAsync();
        return Ok(helpers);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateHelper(int id)
    { var helper = await helperRepository.SelectHelperByIdAsync(id);
        if (helper == null)
        {
            return NotFound();
        }
        if(base.TempData.TryGetValue("validation_errors", out object? validationErrorsObject)) {
            if(validationErrorsObject is string validationErrorsJson) {
                var validationErrors = JsonSerializer.Deserialize<IEnumerable<ValidationErrorModel>>(validationErrorsJson);

                base.ViewData["validation_errors"] = validationErrors;
            }
        }

        
        
        return View(helper);
    }

    [HttpPost]
    [ActionName("UpdateHelper")]
    public async Task<IActionResult> UpdateHelper(Helper newHelper)
    {
        var validationResult = await helperValidator.ValidateAsync(newHelper);

        if (validationResult.IsValid == false)
        {
            base.TempData["validation_errors"] = JsonSerializer.Serialize(validationResult.Errors.Select(error => new ValidationErrorModel
            {
                Message = error.ErrorMessage,
                Property = error.PropertyName
            }));

            return base.RedirectToAction(nameof(UpdateHelper), "Helper");
        }
        else
        {
            var checker = await helperRepository.UpdateHelperAsync(newHelper);
            if (checker)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error updating helper. Please try again.");
                return View(newHelper);
            }
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHelper(int id)
    {
        var checker = await helperRepository.DeleteHelperAsync(id);
        if (checker)
        {
            return Ok();
        }
        else
        {
            return StatusCode(500, "An error occurred while deleting the helper.");
        }
    }
}

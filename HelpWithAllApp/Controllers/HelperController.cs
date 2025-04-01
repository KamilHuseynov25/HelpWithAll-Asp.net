
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
public class HelperController : Controller
{
    private readonly IHelperRepository helperRepository;
    private readonly IHttpLogger logger;
    public HelperController(IHelperRepository helperRepository, IHttpLogger logger)
    {
        this.helperRepository = helperRepository;
        this.logger = logger;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult CreateHelper()
    {
        return View();
    }

    [HttpPost]
    [ActionName("CreateHelper")]
    public async Task<IActionResult> CreateHelper(Helper helper){
        

        var checker = await helperRepository.InsertHelperAsync(helper);
        if(checker)return base.RedirectToAction("Index");
        return StatusCode(500, "An error occurred while updating the helper.");
    }

    [HttpGet]
    [ActionName("ShowAllHelpers")]
    public async Task<IActionResult> ShowAllHelpersAsync(){
        var helpers = await helperRepository.SelectAllHelpersAsync();
        return base.Ok(helpers);
    }
    [HttpGet]
public async Task<IActionResult> UpdateHelper(int id)
{
    var helper = await helperRepository.SelectHelperByIdAsync(id);
    if (helper == null)
    {
        return NotFound();
    }
    return View(helper);
}
    [HttpPost]
    [ActionName("UpdateHelper")]
    public async Task<IActionResult> UpdateHelper(Helper newHelper){
        if (!ModelState.IsValid)
    {
        return View(newHelper);
    }
        var checker = await helperRepository.UpdateHelperAsync(newHelper);
        if(checker){
        return RedirectToAction("Index");
        }
        else{
            ModelState.AddModelError("", "Error updating helper. Please try again.");
            return View(newHelper);
        }
    }
    
    [HttpDelete("{id}")]
    // [ActionName("DeleteHelper")]
    public async Task<IActionResult> DeleteHelper(int id){
        var checker = await helperRepository.DeleteHelperAsync(id);
        if(checker){
        return base.Ok();
        }
        else{
            return StatusCode(500, "An error occurred while updating the helper.");
        }
    }

}

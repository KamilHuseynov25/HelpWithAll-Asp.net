
using HelpWithAllApp.Models;
using HelpWithAllApp.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelpWithAllApp.Controllers;
[Route("[controller]/[action]")]

public class HelperController : Controller
{
    private readonly IHelperRepository helperRepository;

    public HelperController(IHelperRepository helperRepository)
    {
        this.helperRepository = helperRepository;
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
        

        await helperRepository.InsertHelperAsync(helper);
        return base.RedirectToAction("Index");
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
        return View(newHelper); // Return the form with validation messages
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
